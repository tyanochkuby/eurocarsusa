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
        private readonly string _recaptchaSecret;

        public CustomOrderController(ICustomOrderService customOrderService, IConfiguration configuration)
        {
            _customOrderService = customOrderService;
            _recaptchaSecret = configuration["CaptchaSecretKey"];
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm(CustomOrderViewModel customOrderViewModel, string recaptchaResponse)
        {
            if (!await IsReCaptchaValid(recaptchaResponse))
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

        private async Task<bool> IsReCaptchaValid(string recaptchaResponse)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_recaptchaSecret}&response={recaptchaResponse}", null);
                var jsonString = await response.Content.ReadAsStringAsync();
                dynamic jsonData = JObject.Parse(jsonString);
                return jsonData.success == "true";
            }
        }

        public async Task<IActionResult> Order(Guid id)
        {
            CustomOrderViewModel viewModel = await _customOrderService.GetById(id);

            return View(viewModel);
        }


        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TrackOrder(string orderId)
        {
            if (Guid.TryParse(orderId, out Guid id))
            {
                var formViewModel = await _customOrderService.GetById(id);
                if (formViewModel != null)
                {
                    return View("Track", formViewModel);
                }
            }

            ModelState.AddModelError(string.Empty, "Order not found.");
            return View("Track");
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
