using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EuroCarsUSA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IFormService _formService;
        private readonly ICatalogEditingService _catalogEditingService;

        public AdminController(IStatisticsService statisticsService, IFormService formService, ICatalogEditingService catalogEditingService)
        {
            _statisticsService = statisticsService;
            _formService = formService;
            _catalogEditingService = catalogEditingService;
        }

        public async Task<IActionResult> Statistics()
        {
            var model = await _statisticsService.GetCarsStatistics(1, 20);
            return View(model);
        }
        public async Task<IActionResult> EditCatalog()
        {
            var allCars = await _catalogEditingService.GetAll();
            var viewModel = allCars.Select(car => new CatalogEditionViewModel
            {
                Id = car.Id,
                Make = car.Make,
                Type = car.Type,
                Model = car.Model,
                VIN = car.VIN,
                FuelType = car.FuelType,
                EngineVolume = car.EngineVolume,
                Transmission = car.Transmission,
                Price = car.Price,
                Year = car.Year,
                Mileage = car.Mileage,
                Color = car.Color,
                Images = car.Images,
                ImagesJson = JsonSerializer.Serialize(car.Images)

            }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCatalogChanges(List<CatalogEditionViewModel> updatedCarsVM)
        {
            if (ModelState.IsValid)
            {
                var updatedCars = updatedCarsVM.Select(car => new Car
                {
                    Id = car.Id,
                    Make = car.Make,
                    Type = car.Type,
                    Model = car.Model,
                    VIN = car.VIN,
                    FuelType = car.FuelType,
                    EngineVolume = car.EngineVolume,
                    Transmission = car.Transmission,
                    Price = car.Price,
                    Year = car.Year,
                    Mileage = car.Mileage,
                    Color = car.Color,
                    Images = JsonSerializer.Deserialize<List<string>>(car.ImagesJson)
                });
                try
                {
                    await _catalogEditingService.UpdateRange(updatedCars);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(string.Empty, e.Message);
                    return View("EditCatalog", updatedCarsVM);
                }

                // Redirect to the EditCatalog view after successful save
                return RedirectToAction("EditCatalog");
            }

            return View("EditCatalog", updatedCarsVM);
        }
        public async Task<IActionResult> Orders()
        {
            var recievedForms = await _formService.GetAll();
            return View(recievedForms);
        }


    }
}
