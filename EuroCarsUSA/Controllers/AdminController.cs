using Microsoft.AspNetCore.Mvc;

namespace EuroCarsUSA.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Statistics()
        {
            return View();
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
