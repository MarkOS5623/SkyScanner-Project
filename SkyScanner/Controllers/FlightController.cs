using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;

namespace SkyScanner.Controllers
{
    public class FlightController : Controller
    {
        private FlightDbContext _db;
        public FlightController(FlightDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FlightList()
        {
            IEnumerable<Flight> objUserList = _db.Flights;
            return View(objUserList);
        }
        public IActionResult AddFlight()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFlight(Flight obj)
        {
            _db.Flights.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("FlightList");
        }
    }
}
