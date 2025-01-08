using EuroCarsUSA.Data.Enums;
using EuroCarsUSA.Models;
using EuroCarsUSA.Services;
using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using EuroCarsUSA.Views.Home.Components.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace EuroCarsUSA.Controllers
{
    public class CustomOrderController : Controller
    {
        private readonly ICustomOrderService _customOrderService;
        private readonly IRecaptchaService _recaptchaService;

        public CustomOrderController(ICustomOrderService customOrderService, IRecaptchaService recaptchaService)
        {
            _customOrderService = customOrderService;
            _recaptchaService = recaptchaService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm(CustomOrderViewModel customOrderViewModel, string recaptchaResponse)
        {
            if (!await _recaptchaService.IsReCaptchaValid(recaptchaResponse))
            {
                ModelState.AddModelError(string.Empty, "Invalid reCAPTCHA. Please try again.");
                return View("Index", customOrderViewModel);
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("Index", customOrderViewModel);
            }

            var formId = await _customOrderService.SubmitOrderAsync(customOrderViewModel);
            if (formId == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Thanks", new { id = formId });
        }

        public async Task<IActionResult> Order(string id)
        {
            if (Guid.TryParse(id, out Guid guidId))
            {
                var formViewModel = await _customOrderService.GetById(guidId);
                if (formViewModel != null)
                {
                    return View(formViewModel);
                }
            }

            ModelState.AddModelError(string.Empty, "Order not found.");
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TrackOrder(string orderId, string recaptchaResponse)
        {
            if (!await _recaptchaService.IsReCaptchaValid(recaptchaResponse))
            {
                return Json(new { success = false, message = "Invalid reCAPTCHA. Please try again." });
            }
            
            return Json(new { success = true, redirectUrl = Url.Action("Order", new { id = orderId }) });
                
        }

        public IActionResult Thanks(Guid id)
        {
            ViewBag.FormId = id;
            return View();
        }

        public IActionResult Index()
        {
            var model = new CustomOrderViewModel();
            ViewBag.AvailableFilters = new Dictionary<string, IEnumerable<FilterOptionViewModel>>
            {
                { "make", Enum.GetValues(typeof(CarMake)).Cast<CarMake>().Select(m => new FilterOptionViewModel { OriginalValue = m.ToString(), TranslatedValue = m.ToString() }) },
                { "color", Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Select(c => new FilterOptionViewModel { OriginalValue = c.ToString(), TranslatedValue = c.ToString() }) },
                { "carType", Enum.GetValues(typeof(CarType)).Cast<CarType>().Select(t => new FilterOptionViewModel { OriginalValue = t.ToString(), TranslatedValue = t.ToString() }) },
                { "fuelType", Enum.GetValues(typeof(CarFuelType)).Cast<CarFuelType>().Select(f => new FilterOptionViewModel { OriginalValue = f.ToString(), TranslatedValue = f.ToString() }) },
                { "transmission", Enum.GetValues(typeof(CarTransmission)).Cast<CarTransmission>().Select(t => new FilterOptionViewModel { OriginalValue = t.ToString(), TranslatedValue = t.ToString() }) }
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult GetFormComponent(int index)
        {
            var formViewModel = new FormViewModel { Index = index };
            ViewBag.AvailableFilters = new Dictionary<string, IEnumerable<FilterOptionViewModel>>
            {
                { "make", Enum.GetValues(typeof(CarMake)).Cast<CarMake>().Select(m => new FilterOptionViewModel { OriginalValue = m.ToString(), TranslatedValue = m.ToString() }) },
                { "color", Enum.GetValues(typeof(CarColor)).Cast<CarColor>().Select(c => new FilterOptionViewModel { OriginalValue = c.ToString(), TranslatedValue = c.ToString() }) },
                { "carType", Enum.GetValues(typeof(CarType)).Cast<CarType>().Select(t => new FilterOptionViewModel { OriginalValue = t.ToString(), TranslatedValue = t.ToString() }) },
                { "fuelType", Enum.GetValues(typeof(CarFuelType)).Cast<CarFuelType>().Select(f => new FilterOptionViewModel { OriginalValue = f.ToString(), TranslatedValue = f.ToString() }) },
                { "transmission", Enum.GetValues(typeof(CarTransmission)).Cast<CarTransmission>().Select(t => new FilterOptionViewModel { OriginalValue = t.ToString(), TranslatedValue = t.ToString() }) }
            };
            return PartialView("_CustomOrderForm", formViewModel);
        }
    }
}
