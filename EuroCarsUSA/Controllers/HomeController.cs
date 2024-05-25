using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
        private readonly int carsPerLoad = 6;
        private Dictionary<SortOrder, Func<IEnumerable<Car>, IOrderedEnumerable<Car>>> sortFunctions = new Dictionary<SortOrder, Func<IEnumerable<Car>, IOrderedEnumerable<Car>>>
        {
            { SortOrder.ByYear, cars => cars.OrderBy(c => c.Year) },
            { SortOrder.ByYearDesc, cars => cars.OrderByDescending(c => c.Year) },
            { SortOrder.ByMileage, cars => cars.OrderBy(c => c.Mileage) },
            { SortOrder.ByMileageDesc, cars => cars.OrderByDescending(c => c.Mileage) },
            { SortOrder.ByPrice, cars => cars.OrderBy(c => c.Price) },
            { SortOrder.ByPriceDesc, cars => cars.OrderByDescending(c => c.Price) },
        };
        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IDetailPageFormRepository detailPageFormRepository)
        {
            _carRepository = carRepository;
            _detailPageFormRepository = detailPageFormRepository;
            _logger = logger;
            
        }

        public async Task<IActionResult> Index(SortOrder sortOrder, int? minPrice, int? maxPrice, int? minMileage, int? maxMileage, int? minYear, int? maxYear, int? minEngineVolume, int? maxEngineVolume, CarFuelType? fuelType, CarType? carType, CarTransmission? transmission, string color, string make, string model)
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
                FuelType = fuelType,
                CarType = carType,
                Transmission = transmission,
                Color = color,
                Make = !string.IsNullOrEmpty(make) ? Enum.GetValues(typeof(CarMake)).Cast<CarMake>().FirstOrDefault<CarMake>(c => c.ToString().Contains(make) || make.Contains(c.ToString())) : null,
                Model = model
            };
            HttpContext.Session.SetString("CurrentFilters", JsonConvert.SerializeObject(filters));


            IEnumerable<Car> cars = await _carRepository.GetRange(0, carsPerLoad, filters);

            

            if (sortFunctions.ContainsKey(sortOrder))
            {
                cars = sortFunctions[sortOrder](cars);
            }


            
            var carsCount = await _carRepository.GetCount(filters);
            ViewBag.ShowMoreButton = carsCount > carsPerLoad;

            ViewBag.SortOrder = sortOrder;
            return View(cars.ToList());
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
            return Json(new { success = result });
        }

        public async Task<IActionResult> GetMoreCars(int additionalCarsDisplayed)
        {
            CarFilter filters = new CarFilter();
            var sessionData = HttpContext.Session.GetString("CurrentFilters");
            if (!string.IsNullOrEmpty(sessionData))
            {
                filters = JsonConvert.DeserializeObject<CarFilter>(sessionData);
            }
             
            var nextCars = await _carRepository.GetRange(carsPerLoad + additionalCarsDisplayed, carsPerLoad, filters);
            var carsCount = await _carRepository.GetCount(filters);
            var showMoreButton = carsCount > carsPerLoad + additionalCarsDisplayed + carsPerLoad;
            ViewBag.ShowMoreButton = showMoreButton;


            var partialView = PartialView(nextCars);
            return partialView;
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
