using AirBB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBB.Controllers
{
    public class HomeController : Controller
    {
        private AirBBDbcontext _context;
        public HomeController(AirBBDbcontext context)
        {
            _context = context;
        }

        public ViewResult Index(AirBBViewModels model)
        {
            var session = new AirBBSession(HttpContext.Session);

            session.SetActiveLocation(model.ActiveLocation);
            session.SetActiveCheckInDate(model.ActiveCheckInDate);
            session.SetActiveCheckOutDate(model.ActiveCheckOutDate);
            session.SetActiveNoOfGuests(model.ActiveNoOfGuests);

            int? count = session.GetMyReservationCount();
            if (!count.HasValue)
            {
                var cookies = new AirBBCookies(Request.Cookies, Response.Cookies);
                string[] ids = cookies.GetMyReservationIds();

                if (ids.Length > 0)
                {
                    var myReservations = _context.Reservation
                        .Include(r => r.Residence)
                        .ThenInclude(res => res.Location)
                        .Where(r => ids.Contains(r.ReservationId.ToString()))
                        .ToList();

                    session.SetMyReservations(myReservations);
                }
            }

            model.Location = _context.Location.OrderBy(l => l.Name).ToList();

            IQueryable<Residence> query = _context.Residence
                .Include(r => r.Location)
                .OrderBy(r => r.Name);

            if (!string.IsNullOrEmpty(model.ActiveLocation) && model.ActiveLocation.ToLower() != "all")
            {
                query = query.Where(r => r.Location.LocationId.ToString() == model.ActiveLocation);
            }

            if (!string.IsNullOrEmpty(model.ActiveNoOfGuests) && model.ActiveNoOfGuests.ToLower() != "all")
            {
                if (int.TryParse(model.ActiveNoOfGuests, out int guests))
                {
                    query = query.Where(r => r.GuestNumber == guests);
                }
            }

            if (DateTime.TryParse(model.ActiveCheckInDate, out DateTime checkInDate) &&
                DateTime.TryParse(model.ActiveCheckOutDate, out DateTime checkOutDate))
            {
                var reservedResidenceIds = _context.Reservation
                    .Where(res =>
                        res.ReservationStartDate <= checkOutDate &&
                        res.ReservationEndDate >= checkInDate)
                    .Select(res => res.ResidenceId)
                    .Distinct()
                    .ToList();

                query = query.Where(r => !reservedResidenceIds.Contains(r.ResidenceId));
            }

            model.Residence = query.ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Reserve(int id)
        {
            var session = new AirBBSession(HttpContext.Session);
            var cookies = new AirBBCookies(Request.Cookies, Response.Cookies);

            var checkInStr = session.GetActiveCheckInDate();
            var checkOutStr = session.GetActiveCheckOutDate();

            DateTime checkInDate, checkOutDate;

            string dateFormat = "MM/dd/yyyy";
            var culture = System.Globalization.CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(checkInStr, dateFormat, culture, System.Globalization.DateTimeStyles.None, out checkInDate))
                checkInDate = DateTime.Today;

            if (!DateTime.TryParseExact(checkOutStr, dateFormat, culture, System.Globalization.DateTimeStyles.None, out checkOutDate))
                checkOutDate = checkInDate.AddDays(1);

            var reservation = new Reservation
            {
                ResidenceId = id,
                ReservationStartDate = checkInDate,
                ReservationEndDate = checkOutDate
            };

            _context.Reservation.Add(reservation);
            _context.SaveChanges();

            var myReservations = session.GetMyReservations();
            myReservations.Add(reservation);
            session.SetMyReservations(myReservations);
            cookies.SetMyReservationIds(myReservations);

            TempData["Message"] = "✅ Reservation successful! Your residence has been reserved.";

            return RedirectToAction("Index", new
            {
                ActiveLocation = session.GetActiveLocation(),
                ActiveCheckInDate = session.GetActiveCheckInDate(),
                ActiveCheckOutDate = session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = session.GetActiveNoOfGuests()
            });
        }

        public IActionResult MyReservations()
        {
            var session = new AirBBSession(HttpContext.Session);
            var cookies = new AirBBCookies(Request.Cookies, Response.Cookies);
            var reservationIds = cookies.GetMyReservationIds();
            var reservations = _context.Reservation
                .Include(r => r.Residence)
                .ThenInclude(res => res.Location)
                .Where(r => reservationIds.Contains(r.ReservationId.ToString()))
                .ToList();

            var model = new AirBBViewModels
            {
                Reservation = reservations,
                ActiveLocation = session.GetActiveLocation(),
                ActiveCheckInDate = session.GetActiveCheckInDate(),
                ActiveCheckOutDate = session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = session.GetActiveNoOfGuests()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CancelReservation(int id)
        {
            var session = new AirBBSession(HttpContext.Session);

            var reservation = _context.Reservation.Find(id);
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
                _context.SaveChanges();
            }

            var myReservations = session.GetMyReservations();
            var reservationInSession = myReservations.FirstOrDefault(r => r.ReservationId == id);
            if (reservationInSession != null)
            {
                myReservations.Remove(reservationInSession);
                session.SetMyReservations(myReservations);
            }

            var cookies = new AirBBCookies(Request.Cookies, Response.Cookies);
            cookies.RemoveReservationId(id);

            TempData["Message"] = "Reservation cancelled successfully!";
            return RedirectToAction("MyReservations");
        }


        public IActionResult Details(int id)
        {
            var residence = _context.Residence
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null)
                return NotFound();

            var session = new AirBBSession(HttpContext.Session);

            var viewModel = new AirBBViewModels
            {
                Residences = residence,
                ActiveLocation = session.GetActiveLocation(),
                ActiveCheckInDate = session.GetActiveCheckInDate(),
                ActiveCheckOutDate = session.GetActiveCheckOutDate(),
                ActiveNoOfGuests = session.GetActiveNoOfGuests()
            };

            return View(viewModel);
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
