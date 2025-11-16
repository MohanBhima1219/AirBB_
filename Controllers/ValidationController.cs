using Microsoft.AspNetCore.Mvc;
using AirBB.Models;

namespace AirBB.Controllers
{
    public class ValidationController : Controller
    {
        private AirBBDbcontext context;
        public ValidationController(AirBBDbcontext ctx) => context = ctx;
        public JsonResult CheckOwner(int ClientId)
        {
            string msg = Check.OwnerExists(context, ClientId);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okOwner"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
        public JsonResult CheckEmail(string email)
        {
            string msg = Check.EmailExists(context, email);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okEmail"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
