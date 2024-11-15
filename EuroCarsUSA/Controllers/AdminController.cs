using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Text.Json;

namespace EuroCarsUSA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IFormService _formService;
        private readonly ICatalogEditingService _catalogEditingService;
        private readonly IStringLocalizer<AdminController> _localizer;

        public AdminController(IStatisticsService statisticsService, IFormService formService, ICatalogEditingService catalogEditingService, IStringLocalizer<AdminController> localizer)
        {
            _statisticsService = statisticsService;
            _formService = formService;
            _catalogEditingService = catalogEditingService;
            _localizer = localizer;
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
        public async Task<IActionResult> SaveCatalogChanges(List<CatalogEditionViewModel> updatedCarsVM, string deletedCarsIds)
        {
            updatedCarsVM.ForEach(car => car.Images = JsonSerializer.Deserialize<List<string>>(car.ImagesJson));

            var deletedCarsIdsList = new List<Guid>();
            if (deletedCarsIds is not null)
            {
                deletedCarsIdsList = JsonSerializer.Deserialize<List<Guid>>(deletedCarsIds);
            }

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
                Images = car.Images,
            }).ToList();

            try
            {
                await _catalogEditingService.UpdateRange(updatedCars);
                await _catalogEditingService.DeleteRange(deletedCarsIdsList);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View("EditCatalog", updatedCarsVM);
            }

            // Redirect to the EditCatalog view after successful save
            return RedirectToAction("EditCatalog");
        }
        public async Task<IActionResult> Orders()
        {
            var recievedForms = await _formService.GetAll();
            return View(recievedForms);
        }

        [HttpPut]
        public async Task<IActionResult> SetOrderStatus(Guid id, FormStatus status)
        {
            if (await _formService.UpdateStatus(id, status))
                return Ok();
            return BadRequest();
        }
    }
}
