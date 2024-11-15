﻿using EuroCarsUSA.Services.Interfaces;
using EuroCarsUSA.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EuroCarsUSA.Controllers
{
    public class CustomOrderController : Controller
    {
        private readonly IFormService _formService;
        public CustomOrderController(IFormService formService)
        {
            _formService = formService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm(FormViewModel formViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View("Index", formViewModel);
            }

            var formId = await _formService.SubmitFormAsync(formViewModel);
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
                var formViewModel = await _formService.GetById(id);
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
