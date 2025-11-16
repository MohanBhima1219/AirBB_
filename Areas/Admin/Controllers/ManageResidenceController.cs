using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/[controller]/[action]")]
    public class ManageResidenceController : Controller
    {
        private AirBBDbcontext context { get; set; }
        public ManageResidenceController(AirBBDbcontext ctx) => context = ctx;
        public IActionResult List()
        {
            var residence = context.Residence
                .Include(r => r.Location)
                .Include(r => r.Client)
                .OrderBy(m => m.Name)
                .ToList();

            return View(residence);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            ViewBag.Locations = context.Location
                .OrderBy(l => l.Name)
                .Select(l => new SelectListItem
                {
                    Value = l.LocationId.ToString(),
                    Text = l.Name
                })
                .ToList();

            ViewBag.Clients = context.Client
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.Name
                })
                .ToList();
            return View("Edit", new Residence());
        }

        [HttpPost]
        public IActionResult Edit(Residence residence)
        {
            if (TempData["okEmail"] == null)
            {
                string msg = Check.OwnerExists(context, residence.ClientId);
                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(residence.ClientId), msg);
                    TempData["message"] = "Please fix the error";
                }
            }

            if (ModelState.IsValid)
            {
                if (residence.ResidenceId == 0)
                {
                    context.Residence.Add(residence);
                    TempData["message"] = $"{residence.Name} added successfully.";
                }
                else
                {
                    context.Residence.Update(residence);
                    TempData["message"] = $"{residence.Name} updated successfully.";
                }

                context.SaveChanges();
                return RedirectToAction("List", "ManageResidence");
            }
            else
            {
                ViewBag.Locations = context.Location
                    .OrderBy(l => l.Name)
                    .Select(l => new SelectListItem
                    {
                        Value = l.LocationId.ToString(),
                        Text = l.Name
                    })
                    .ToList();

                ViewBag.Clients = context.Client
                    .OrderBy(c => c.Name)
                    .Select(c => new SelectListItem
                    {
                        Value = c.ClientId.ToString(),
                        Text = c.Name
                    })
                    .ToList();
                ViewBag.Action = residence.ResidenceId == 0 ? "Add" : "Edit";
                return View(residence);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.action = "Edit";
            ViewBag.Locations = context.Location
                .OrderBy(l => l.Name)
                .Select(l => new SelectListItem
                {
                    Value = l.LocationId.ToString(),
                    Text = l.Name
                })
                .ToList();

            ViewBag.Clients = context.Client
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Value = c.ClientId.ToString(),
                    Text = c.Name
                })
                .ToList();
            var residence = context.Residence.Find(id);
            return View(residence);
        }

        [HttpPost]
        public IActionResult Delete(Residence residence)
        {
            context.Residence.Remove(residence);
            TempData["message"] = $"{residence.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var residence = context.Residence.Find(id);
            return View(residence);
        }
    }
}
