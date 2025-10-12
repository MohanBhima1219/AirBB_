using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Area: Main, Controller: Residence, Action: List, ID: {id}");
        }
    }
}
