using EuroCarsUSA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EuroCarsUSA.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public AdminController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
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
        public IActionResult Orders()
        {
            return View();
        }
    }
}
