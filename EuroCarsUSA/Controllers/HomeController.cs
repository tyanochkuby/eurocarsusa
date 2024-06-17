using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Data.Services;
using EuroCarsUSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;

namespace EuroCarsUSA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IDetailPageFormRepository _detailPageFormRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private const int carsPerLoad = 6;
        
        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IDetailPageFormRepository detailPageFormRepository, IEmailService emailService)
        {
            _carRepository = carRepository;
            _detailPageFormRepository = detailPageFormRepository;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index(string sortOrder, int? minPrice, int? maxPrice, int? minMileage, int? maxMileage, int? minYear, int? maxYear, int? minEngineVolume, int? maxEngineVolume, string fuelType, string carType, string transmission, string color, string make, string model)
        {
            //ViewBag.YearSortParm = sortOrder == SortOrder.ByYear ? SortOrder.ByYearDesc : SortOrder.ByYear;
            //ViewBag.MileageSortParm = sortOrder == SortOrder.ByMileage ? SortOrder.ByMileageDesc : SortOrder.ByMileage;
            //ViewBag.PriceSortParm = sortOrder == SortOrder.ByPrice ? SortOrder.ByPriceDesc : SortOrder.ByPrice;
            

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
                FuelType = !string.IsNullOrEmpty(fuelType) ?
                    Enum.GetValues(typeof(CarFuelType))
                        .Cast<CarFuelType>()
                        .Where(m => fuelType.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList()
                    : new List<CarFuelType>(),
                CarType = !string.IsNullOrEmpty(carType) ?
                    Enum.GetValues(typeof(CarType))
                        .Cast<CarType>()
                        .Where(m => carType.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList()
                    : new List<CarType>(),
                Transmission = !string.IsNullOrEmpty(transmission) ?
                    Enum.GetValues(typeof(CarTransmission))
                        .Cast<CarTransmission>()
                        .Where(m => transmission.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList()
                    : new List<CarTransmission>(),
                Color = !string.IsNullOrEmpty(color) ?
                    Enum.GetValues(typeof(CarColor))
                        .Cast<CarColor>()
                        .Where(m => color.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList()
                    : new List<CarColor>(),
                Make = !string.IsNullOrEmpty(make) ? 
                    Enum.GetValues(typeof(CarMake))
                        .Cast<CarMake>()
                        .Where(m => make.ToLower().Split(new[] { ' ', ',' }).Any(s => s == m.ToString().ToLower()))
                        .ToList() 
                    : new List<CarMake>(),
                Model = model,
            };
            SortOrder sortOrderEnum;
            if (Enum.TryParse(sortOrder, true, out sortOrderEnum))
            {
                filters.SortOrder = sortOrderEnum;
            }

            HttpContext.Session.SetString("CurrentFilters", JsonConvert.SerializeObject(filters));

            var availableFilters = await _carRepository.GetAvailableFilters();

            ViewBag.AvailableFilters = availableFilters;

            return View();
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            Car car = await _carRepository.GetById(id);
            DetailPageForm detailPageForm = new DetailPageForm { CarId = car.Id };
            ViewBag.Car = car;
            return View(detailPageForm);
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

        public async Task<IActionResult> GetCars(int carsDisplayed)
        {
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


            var partialView = PartialView(nextCars);
            return partialView;
        }

        public async Task<IActionResult> Likes()
        {
            StringValues values;
            HttpContext.Request.Headers.TryGetValue("Cookie", out values);
            var cookies = values.ToString().Split(';').ToList();
            var result = cookies.Select(c => new { Key = c.Split('=')[0].Trim(), Value = c.Split('=')[1].Trim() }).ToList();
            var likesCookieValue = result.Where(r => r.Key == "likes").FirstOrDefault().Value;
            List<Guid> likes = new List<Guid>();
            if (!string.IsNullOrEmpty(likesCookieValue))
            {
                likes = JsonConvert.DeserializeObject<List<Guid>>(likesCookieValue);
            }
            List<Car> likedCars = new List<Car>();
            foreach(Guid guid in likes)
            {
                Car car = await _carRepository.GetById(guid);
                if (car != null)
                {
                    likedCars.Add(car);
                }
            }
            return View(likedCars);
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
