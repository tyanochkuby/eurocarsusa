using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Extensions;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using static NuGet.Packaging.PackagingConstants;

namespace EuroCarsUSA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IDetailPageFormRepository _detailPageFormRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ICookieService _cookieService;
        private readonly IStatisticsService _statisticsService;
        private readonly ICompositeViewEngine _viewEngine;

        private const int carsPerLoad = 6;
        private Dictionary<string, List<FilterOptionViewModel>> _availableFilters; 
        
        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IDetailPageFormRepository detailPageFormRepository, IEmailService emailService, IStringLocalizer<HomeController> localizer, ICookieService cookieService, IStatisticsService statisticsService, ICompositeViewEngine viewEngine)
        {
            _carRepository = carRepository;
            _detailPageFormRepository = detailPageFormRepository;
            _logger = logger;
            _emailService = emailService;
            _localizer = localizer;
            _cookieService = cookieService;
            _statisticsService = statisticsService;
            _viewEngine = viewEngine;
        }

        public async Task<IActionResult> Index(string sortOrder, int? minPrice, int? maxPrice, int? minMileage, int? maxMileage, int? minYear, int? maxYear, int? minEngineVolume, int? maxEngineVolume, string fuelType, string carType, string transmission, string color, string make, string model)
        {
            ViewData["SortBy"] = _localizer["SortBy"];
            var filters = new CarFilter()
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                MinMileage = minMileage,
                MaxMileage = maxMileage,
                MinYear = minYear,
                MaxYear = maxYear,
                MinEngineVolume = minEngineVolume,
                MaxEngineVolume = maxEngineVolume,
                FuelType = EnumHelper.GetEnumListFromString<CarFuelType>(fuelType),
                CarType = EnumHelper.GetEnumListFromString<CarType>(carType),
                Transmission = EnumHelper.GetEnumListFromString<CarTransmission>(transmission),
                Color = EnumHelper.GetEnumListFromString<CarColor>(color),
                Make = EnumHelper.GetEnumListFromString<CarMake>(make),
                Model = model,
            };
            SortOrder sortOrderEnum;
            if (Enum.TryParse(sortOrder, true, out sortOrderEnum))
            {
                filters.SortOrder = sortOrderEnum;
            }

            HttpContext.Session.SetString("CurrentFilters", JsonConvert.SerializeObject(filters));

            _availableFilters = await _carRepository.GetAvailableFilters(_localizer);

            ViewBag.AvailableFilters = _availableFilters;

            return View();
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            await _statisticsService.ViewCar(id);
            Car car = await _carRepository.GetById(id);

            DetailViewModel viewModel = new()
            {
                Car = car,
                DetailPageForm = new DetailPageForm { CarId = car.Id }
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendDetailPageForm(DetailPageForm form)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            bool result = await _detailPageFormRepository.Add(form);

            if (result)
            {
                var car = await _carRepository.GetById(form.CarId);

                string phoneNumberMessage = string.IsNullOrEmpty(form.PhoneNumber) ? "No phone number provided" : $"Phone: {form.PhoneNumber}";
                string emailMessage = string.IsNullOrEmpty(form.Email) ? "No email provided" : $"Email: {form.Email}";
                string message = string.IsNullOrEmpty(form.Message) ? "No message provided" : $"Message: {form.Message}";
                string emailBody = $"Name: {form.Name}\n{emailMessage}\n{phoneNumberMessage}\n{message}\n\nCar: {car.Make.ToString()} {car.Model} {car.Year}";
                try
                {
                    //email to client
                    if (!string.IsNullOrEmpty(form.Email))
                        _emailService.SendEmail(form.Email, "EuroCarsUSA - Form received", $"Hello!\n\nWe just received you form and we're now asking you for a little bit of patience. We will contact you shortly.\n\nAnd thank you for your interest in our car - {car.Make} {car.Model}.\n\nBest regards\nEuroCarsUSA team");
                    //email to admin
                    _emailService.SendEmail("romyud1994@gmail.com", "New form submitted", emailBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return Json(new { success = true, errors = new List<string> { "Failed to send email" } });
                }
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, errors = new List<string> { "Failed to submit form" } });
            }
        }

        public async Task<IActionResult> GetCars(int carsDisplayed, string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);
            }
            CarFilter filters = new CarFilter();
            var sessionData = HttpContext.Session.GetString("CurrentFilters");
            if (!string.IsNullOrEmpty(sessionData))
            {
                filters = JsonConvert.DeserializeObject<CarFilter>(sessionData);
            }
             
            var nextCars = await _carRepository.GetRange(carsDisplayed, carsPerLoad, filters, filters.SortOrder);
            var carsCount = await _carRepository.GetCount(filters);
            var showMoreButton = carsCount > carsDisplayed + carsPerLoad;
            ViewBag.ShowMoreButton = showMoreButton;

            var likedCars = _cookieService.GetUserLikedCars();
            var model = nextCars.Select(c => CarViewModel.FromCar(c, likedCars)).ToList();

            var partialView = PartialView(model);
            return partialView;
        }

        public async Task<IActionResult> Likes(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);
            }

            var likes = _cookieService.GetUserLikedCars();
            List<Car> likedCars = new List<Car>();
            foreach(Guid guid in likes)
            {
                Car car = await _carRepository.GetById(guid);
                if (car != null)
                {
                    likedCars.Add(car);
                }
            }
            var model = likedCars.Select(c => CarViewModel.FromCar(c, likes)).ToList();
            return View(model);
        }

        public async Task<IActionResult> GetFilterOptions(string filterType, string culture)
        {
            if (!string.IsNullOrEmpty(culture))
            {
                CultureInfo.CurrentCulture = new CultureInfo(culture);
                CultureInfo.CurrentUICulture = new CultureInfo(culture);
            }
            
            if (_availableFilters == null)
            {
                _availableFilters = await _carRepository.GetAvailableFilters(_localizer);
            }

            if (!_availableFilters.ContainsKey(filterType))
            {
                return NotFound($"Filter type '{filterType}' is not available.");
            }

            return PartialView("~/Views/Home/Components/_MobileFilterSelect.cshtml", _availableFilters[filterType]);
        }

        public IActionResult PressedLikeButton(Guid carId)
        {
            var html = RenderPartialViewToString("~/Views/Home/Components/Buttons/LikePressed.cshtml", carId);
            return Json(new { success = true, html });
        }
        public IActionResult UnpressedLikeButton(Guid carId)
        {
            var html = RenderPartialViewToString("~/Views/Home/Components/Buttons/LikeUnpressed.cshtml", carId);
            return Json(new { success = true, html });
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            var returnUrl = Request.Headers["Referer"].ToString();
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> LikeCar(Guid carId)
        {
            await _statisticsService.LikeCar(carId);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UnlikeCar(Guid carId)
        {
            await _statisticsService.UnlikeCar(carId);
            return Json(new { success = true });
        }

        public IActionResult BackToIndexWithFilters()
        {
            var sessionData = HttpContext.Session.GetString("CurrentFilters");
            if (!string.IsNullOrEmpty(sessionData))
            {
                var filters = JsonConvert.DeserializeObject<CarFilter>(sessionData);
                return RedirectToAction("Index", filters);
            }
            return RedirectToAction("Index");
        }
        private string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.GetView(null, viewName, false);
                if (!viewResult.Success)
                {
                    viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                }

                if (!viewResult.Success)
                {
                    _logger.LogError($"View '{viewName}' not found.");
                    throw new InvalidOperationException($"View '{viewName}' not found.");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                viewResult.View.RenderAsync(viewContext).Wait();
                return writer.GetStringBuilder().ToString();
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
