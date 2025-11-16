using AirBB.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin/[controller]/[action]")]
    public class ManageClientController : Controller
    {
        private AirBBDbcontext context { get; set; }
        public ManageClientController(AirBBDbcontext ctx) => context = ctx;
        public IActionResult List()
        {
            var client = context.Client
                .OrderBy(m => m.Name)
                .ToList();

            return View(client);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Client());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var client = context.Client.Find(id);
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.ClientId == 0)
                {
                    context.Client.Add(client);
                    TempData["message"] = $"{client.Name} Added Successfully";
                }
                else
                {
                    context.Client.Update(client);
                    TempData["message"] = $"{client.Name} Updated Successfully";
                }
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                ViewBag.action = (client.ClientId == 0) ? "Add" : "Edit";
                return View("Edit", client);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = context.Client.Find(id);
            return View(client);
        }

        [HttpPost]
        public IActionResult Delete(Client client)
        {
            context.Client.Remove(client);
            TempData["message"] = $"{client.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("List", "ManageClient");
        }
    }
}
