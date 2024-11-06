using EuroCarsUSA.Data.Interfaces;
using EuroCarsUSA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EuroCarsUSA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IFormService _formService;
        public AdminController(IStatisticsService statisticsService, IFormService formService)
        {
            _statisticsService = statisticsService;
            _formService = formService;
        }

        public async Task<IActionResult> Statistics()
        {
            var model = await _statisticsService.GetCarsStatistics(1, 20);
            return View(model);
        }
        public IActionResult EditCatalog()
        {
            return View();
        }
        public async Task<IActionResult> Orders()
        {
            var recievedForms = await _formService.GetAll();
            return View(recievedForms);
        }


    }
}
