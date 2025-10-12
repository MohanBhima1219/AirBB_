using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Support()
        {
            return Content("Area: Main, Controller: Home, Action: Support");
        }
        public IActionResult CancellationPolicy()
        {
            return Content("Area: Main, Controller: Home, Action: CancellationPolicy");
        }
        public IActionResult Terms()
        {
            return Content("Area: Main, Controller: Home, Action: Terms & Condition");
        }
        public IActionResult Cookies()
        {
            return Content("Area: Main, Controller: Home, Action: Cookie Policies");
        }
    }
}
