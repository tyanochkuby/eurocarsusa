using EuroCarsUSA.Data;
using EuroCarsUSA.Data.Enum;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;

namespace EuroCarsUSA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository)
        {
            _carRepository = carRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index(SortOrder sortOrder, int? minPrice, int? maxPrice, int? minYear, int? maxYear, int? minEngineVolume, int? maxEngineVolume, CarFuelType? fuelType, CarTransmission? transmission, string color, string make)
        {
            ViewBag.YearSortParm = sortOrder == SortOrder.ByYear ? SortOrder.ByYearDesc : SortOrder.ByYear;
            ViewBag.MileageSortParm = sortOrder == SortOrder.ByMileage ? SortOrder.ByMileageDesc : SortOrder.ByMileage;
            ViewBag.PriceSortParm = sortOrder == SortOrder.ByPrice ? SortOrder.ByPriceDesc : SortOrder.ByPrice;

            IEnumerable<Car> cars = await _carRepository.GetAll();

            #region filters and sorting
            if(!string.IsNullOrEmpty(make))
            {
                cars = cars.Where(c => (c.Make.ToString().Contains(make) || make.Contains(c.Make.ToString())));
            }
            if (minPrice.HasValue)
            {
                cars = cars.Where(c => c.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                cars = cars.Where(c => c.Price <= maxPrice.Value);
            }
            if(minYear.HasValue)
            {
                cars = cars.Where(c => c.Year >= minYear.Value);
            }
            if (maxYear.HasValue)
            {
                cars = cars.Where(c => c.Year <= maxYear.Value);
            }
            if (minEngineVolume.HasValue)
            {
                cars = cars.Where(c => c.EngineVolume >= minEngineVolume.Value);
            }
            if (maxEngineVolume.HasValue)
            {
                cars = cars.Where(c => c.EngineVolume <= maxEngineVolume.Value);
            }
            if (fuelType.HasValue)
            {
                cars = cars.Where(c => c.FuelType == fuelType.Value);
            }
            if(transmission.HasValue)
            {
                cars = cars.Where(c => c.Transmission == transmission.Value);
            }
            if (!string.IsNullOrEmpty(color))
            {
                cars = cars.Where(c => c.Color == color);
            }
            


            ViewBag.MinPrice = minPrice; ViewBag.MaxPrice = maxPrice;
            ViewBag.FuelType = fuelType;
            ViewBag.Color = color;
            ViewBag.Make = make;
            ViewBag.MinYear = minYear; ViewBag.MaxYear = maxYear;
            ViewBag.MinEngineVolume = minEngineVolume; ViewBag.MaximumEngineVolume = maxEngineVolume;
            ViewBag.Transmission = transmission;

            ViewBag.SortOrder = sortOrder;

            switch (sortOrder)
            {
                case SortOrder.ByYear: 
                    cars = cars.OrderBy(c => c.Year);
                    break;
                case SortOrder.ByYearDesc:
                    cars = cars.OrderByDescending(c => c.Year);
                    break;
                case SortOrder.ByMileage:
                    cars = cars.OrderBy(c => c.Mileage);
                    break;
                case SortOrder.ByMileageDesc:
                    cars = cars.OrderByDescending(c => c.Mileage);
                    break;
                case SortOrder.ByPrice:
                    cars = cars.OrderBy(c => c.Price);
                    break;
                case SortOrder.ByPriceDesc:
                    cars = cars.OrderByDescending(c => c.Price);
                    break;
            }

            

            #endregion



            return View(cars.ToList());
        }


        public async Task<IActionResult> Detail(Guid id)
        {
            Car car = await _carRepository.GetByIdAsync(id);
            return View(car);
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
