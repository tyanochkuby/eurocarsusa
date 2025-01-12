using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Extensions;
using EuroCarsUSA.Models;
using EuroCarsUSA.Resources;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;

namespace EuroCarsUSA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IDetailPageFormRepository _detailPageFormRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly IStringLocalizer<HomeController> _translator;
        private readonly Localizer _localizer;
        private readonly ICookieService _cookieService;
        private readonly IStatisticsService _statisticsService;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IRecommendationService _recommendationService;
        private readonly IRecaptchaService _recaptchaService;

        private const int carsPerLoad = 6;

        private Dictionary<string, List<FilterOptionViewModel>> _availableFilters; 
        
        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IDetailPageFormRepository detailPageFormRepository, IEmailService emailService, IStringLocalizer<HomeController> translator, ICookieService cookieService, IStatisticsService statisticsService, ICompositeViewEngine viewEngine, IRecommendationService recommendationService, Localizer localizer, IRecaptchaService recaptchaService)
        {
            _carRepository = carRepository;
            _detailPageFormRepository = detailPageFormRepository;
            _logger = logger;
            _emailService = emailService;
            _translator = translator;
            _cookieService = cookieService;
            _statisticsService = statisticsService;
            _viewEngine = viewEngine;
            _recommendationService = recommendationService;
            _localizer = localizer;
            _recaptchaService = recaptchaService;
        }

        public async Task<IActionResult> Catalog(string sortOrder, int? minPrice, int? maxPrice, int? minMileage, int? maxMileage, int? minYear, int? maxYear, int? minEngineVolume, int? maxEngineVolume, string fuelType, string carType, string transmission, string color, string make, string model)
        {
            ViewData["SortBy"] = _translator["SortBy"];
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
                Status = new List<CarStatus> { CarStatus.Available, CarStatus.Recommended }
            };
            SortOrderType sortOrderEnum;
            if (Enum.TryParse(sortOrder, true, out sortOrderEnum))
            {
                filters.SortOrder = sortOrderEnum;
            }

            HttpContext.Session.SetString("CurrentFilters", JsonConvert.SerializeObject(filters));

            _availableFilters = await _carRepository.GetAvailableFilters(_translator);

            ViewBag.AvailableFilters = _availableFilters;

            var shippingCars = await _carRepository.GetAll([CarStatus.Shipping]);
            var soldCars = await _carRepository.GetAll([CarStatus.Sold]);
            var viewModel = new CatalogViewModel
            {
                ShippingCars = shippingCars.Select(c => CarCardViewModel.FromCar(c)).ToList(),
                SoldCars = soldCars.Select(c => CarCardViewModel.FromCar(c)).ToList()
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            await _statisticsService.ViewCar(id);
            Car car = await _carRepository.GetById(id);

            var recomendedCars = await _recommendationService.GetFirstNCars(4);
            CarCardViewModel.likedCars = _cookieService.GetUserLikedCars();
            var recomendedCardsViewModels = recomendedCars.Select(c => CarCardViewModel.FromCar(c)).ToList();

            DetailViewModel viewModel = new()
            {
                Car = car,
                DetailPageForm = new DetailPageForm { CarId = car.Id },
                RecomendedCar = recomendedCardsViewModels,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SendDetailPageForm(DetailPageFormViewModel form)
        {
            if (!await _recaptchaService.IsReCaptchaValid(form.RecaptchaToken))
            {
                return Json(new { success = false, errors = new List<string>() { "Invalid reCAPTCHA. Please try again." } });
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors = errors });
            }

            var formDTO = new DetailPageForm
            {
                CarId = form.CarId,
                Name = form.Name,
                Email = form.Email,
                PhoneNumber = form.PhoneNumber,
                Message = form.Message,
            };
            bool result = await _detailPageFormRepository.Add(formDTO);

            if (result)
            {
                var car = await _carRepository.GetById(form.CarId);

                string phoneNumber = string.IsNullOrEmpty(form.PhoneNumber) ? _localizer.EmptyEmailField : form.PhoneNumber;
                string email = string.IsNullOrEmpty(form.Email) ? _localizer.EmptyEmailField : form.Email;
                string message = string.IsNullOrEmpty(form.Message) ? _localizer.EmptyEmailField : form.Message;
                try
                {
                    //email to client
                    if (!string.IsNullOrEmpty(form.Email))
                        await _emailService.SendEmail(form.Email, _localizer.DetailPageEmailSubject, 
                            string.Format(_localizer.DetailPageEmailBody, car.Make.ToString(), car.Model));
                    //email to admin
                    await _emailService.SendEmail(_emailService.AdminEmail, _localizer.AdminDetailPageEmailSubject, 
                        string.Format(_localizer.AdminDetailPageEmailBody, car.Make.ToString(), car.Model, car.Year.ToString(), form.Name, email, phoneNumber, message));
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

        public async Task<IActionResult> GetCars(int carsDisplayed)
        { 
            CarFilter filters = new CarFilter();
            var sessionData = HttpContext.Session.GetString("CurrentFilters");
            if (!string.IsNullOrEmpty(sessionData))
            {
                filters = JsonConvert.DeserializeObject<CarFilter>(sessionData);
            }

            filters.Status = new List<CarStatus> { CarStatus.Available, CarStatus.Recommended };

            var nextCars = await _carRepository.GetRange(carsDisplayed, carsPerLoad, filters, filters.SortOrder);
            var carsCount = await _carRepository.GetCount(filters);
            var showMoreButton = carsCount > carsDisplayed + carsPerLoad;
            ViewBag.ShowMoreButton = showMoreButton;

            CarCardViewModel.likedCars = _cookieService.GetUserLikedCars();
            var model = nextCars.Select(c => CarCardViewModel.FromCar(c)).ToList();

            var partialView = PartialView(model);
            return partialView;
        }

        public async Task<IActionResult> Likes()
        { 
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
            CarCardViewModel.likedCars = likes;
            var model = likedCars.Select(c => CarCardViewModel.FromCar(c)).ToList();
            return View(model);
        }

        public async Task<IActionResult> Index()
        { 
            var likes = _cookieService.GetUserLikedCars();
            CarCardViewModel.likedCars = likes;

            var carsWDrodze = await _carRepository.GetRange(0, 4, new() { Status = [CarStatus.Shipping] }, null);
            var recomendedCars = await _recommendationService.GetFirstNCars(6);
            var soldCars = await _carRepository.GetRange(0, 4, new() { Status = [CarStatus.Sold] }, null);

            var cardsWDrodzeViewModel = carsWDrodze.Select(c => CarCardViewModel.FromCar(c)).ToList();
            var recomendedCadrsViewModel = recomendedCars.Select(c => CarCardViewModel.FromCar(c)).ToList();
            var lastAddedCar = await _recommendationService.GetLastAddedCar();
            var soldCarsViewModel = soldCars.Select(c => CarCardViewModel.FromCar(c)).ToList();

            MainlViewModel viewModel = new()
            {
                ShippingCars = cardsWDrodzeViewModel,
                RecomendedCars = recomendedCadrsViewModel,
                LastAddedCar = lastAddedCar == null ? null : CarCardViewModel.FromCar(lastAddedCar),
                SoldCars = soldCarsViewModel
            };

            return View(viewModel);
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
                _availableFilters = await _carRepository.GetAvailableFilters(_translator);
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

            var referer = Request.Headers["Referer"].ToString();
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            if (string.IsNullOrEmpty(referer) || !referer.StartsWith(baseUrl))
            {
                referer = Url.Action("Index", "Home");
            }

            return Redirect(referer);
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

        public IActionResult BackToCatalogWithFilters()
        {
            var sessionData = HttpContext.Session.GetString("CurrentFilters");
            if (!string.IsNullOrEmpty(sessionData))
            {
                var filters = JsonConvert.DeserializeObject<CarFilter>(sessionData);
                return RedirectToAction("Catalog", filters);
            }
            return RedirectToAction("Catalog");
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
