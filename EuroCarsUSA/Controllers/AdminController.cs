using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EuroCarsUSA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly ICustomOrderService customOrderService;
        private readonly ICatalogEditingService _catalogEditingService;

        public AdminController(IStatisticsService statisticsService, ICustomOrderService customOrderService, ICatalogEditingService catalogEditingService)
        {
            _statisticsService = statisticsService;
            this.customOrderService = customOrderService;
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
                ImagesJson = JsonSerializer.Serialize(car.Images),
                Status = car.Status,
            }).ToList();
            return View(viewModel);
        }

        public async Task<IActionResult> GetNewCatalogRow(int index) 
        {
            var car = new CatalogEditionViewModel();
            return PartialView("~/Views/Shared/_EditCar.cshtml", new Tuple<CatalogEditionViewModel, int>(car, index));
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
            var recievedForms = await customOrderService.GetAll();
            List<CustomOrderViewModel> formstatusSent = new List<CustomOrderViewModel>();
            List<CustomOrderViewModel> formstatusOpened = new List<CustomOrderViewModel>();
            List<CustomOrderViewModel> formstatusClosed = new List<CustomOrderViewModel>();
            foreach (var form in recievedForms)
            {
                if (form.Status == OrderStatus.Sent)
                {
                    formstatusSent.Add(form);
                }
                else if (form.Status == OrderStatus.Opened) {
                    formstatusOpened.Add(form);
                }
                else
                {
                    formstatusClosed.Add(form);
                }
            }
            OrdersViewModel orders = new OrdersViewModel();
            orders.Sent = formstatusSent;
            orders.Opened = formstatusOpened;
            orders.Closed = formstatusClosed;
            return View(orders);
        }

        [HttpPut]
        public async Task<IActionResult> SetOrderStatus(Guid id, OrderStatus status)
        {
            if (await customOrderService.UpdateStatus(id, status))
                return Ok();
            return BadRequest();
        }
    }
}
