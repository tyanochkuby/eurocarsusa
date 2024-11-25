using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroCarsUSA.Controllers
{
    public class CustomOrderController : Controller
    {
        private readonly ICustomOrderService _customOrderService;
        public CustomOrderController(ICustomOrderService customOrderService)
        {
            _customOrderService = customOrderService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm(CustomOrderViewModel customOrderViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("Index", customOrderViewModel);
            }

            var formId = await _customOrderService.SubmitFormAsync(customOrderViewModel);
            if (formId == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Thanks", new { id = formId });
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
            return View();
        }
    }
}
