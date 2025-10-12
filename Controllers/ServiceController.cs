using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Area: Main, Controller: Service, Action: List, ID: {id}");
        }
    }
}
