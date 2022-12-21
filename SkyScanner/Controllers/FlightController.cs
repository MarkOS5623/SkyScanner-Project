using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;

namespace SkyScanner.Controllers
{
    public class FlightController : Controller
    {
        private FlightDbContext _db;
        public FlightController(FlightDbContext db) //setting up db context
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FlightList() //GET method for FlightList View
        {
            IEnumerable<Flight> objUserList = _db.Flights;
            return View(objUserList);
        }
        public IActionResult AddFlight() //GET method for AddFlight
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFlight(Flight obj) //POST method for AddFlight
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("FlightList");
            }
            return View(obj);
        }

        public IActionResult EditFlight(string? flightid) //GET method for EditFlight
        {
            if (flightid == null) return NotFound();
            var flightFromDb = _db.Flights.Find(flightid);
            if(flightFromDb == null) return NotFound();
            return View(flightFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFlight(Flight obj) //POST method for EditFlight
        {
            if (ModelState.IsValid)
            {
                _db.Flights.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("FlightList");
            }
            return View(obj);
        }
        public IActionResult DeleteFlight(string? flightid) //GET method for DeleteFlight
        {
            if (flightid == null) return NotFound();
            var flightFromDb = _db.Flights.Find(flightid);
            if (flightFromDb == null) return NotFound();
            return View(flightFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFlight(Flight obj) //POST method for DeleteFlight
        {
            if (obj != null)
            {
                _db.Flights.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction("FlightList");
            }
            return NotFound();
        }
    }
}
