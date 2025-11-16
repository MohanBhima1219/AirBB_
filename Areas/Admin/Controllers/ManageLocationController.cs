using AirBB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/[controller]/[action]")]
    public class ManageLocationController : Controller
    {
        private AirBBDbcontext context { get; set; }
        public ManageLocationController(AirBBDbcontext ctx) => context = ctx;
        public IActionResult List()
        {
            var location = context.Location
                .OrderBy(m => m.Name)
                .ToList();

            return View(location);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            return View("Edit", new Location());
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                if (location.LocationId == 0)
                {
                    context.Location.Add(location);
                    TempData["message"] = $"{location.Name} Added Successfully";
                }
                else
                {
                    context.Location.Update(location);
                    TempData["message"] = $"{location.Name} Updated Successfully";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.action = (location.LocationId == 0) ? "Add" : "Edit";
                return View("Edit", location);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.action = "Edit";
            var location = context.Location.Find(id);
            return View(location);
        }

        [HttpPost]
        public IActionResult Delete(Location location)
        {
            context.Location.Remove(location);
            TempData["message"] = $"{location.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var location = context.Location.Find(id);
            return View(location);
        }
    }
}
