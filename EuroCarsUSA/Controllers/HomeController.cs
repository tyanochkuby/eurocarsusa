using EuroCarsUSA.Data;
using EuroCarsUSA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EuroCarsUSA.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.ToList();

            return View(cars);
        }


        public IActionResult Detail(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);
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
