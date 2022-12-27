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
            _db.Seats = db.Seats;
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
        public IActionResult BookSeat(string? flightid) //GET method for BookSeat View
        {
            if (flightid == null) return NotFound();
            var flightFromDb = _db.Flights.Find(flightid);
            if (flightFromDb == null) return NotFound();
            var SeatsFromDb = _db.Seats.Where(x => x.Flight_num == flightid && x.Booked == true).ToList();
            return View(flightFromDb);
        }
        [HttpPost]
        public IActionResult BookSeat(string[] Seats, string? FlightID) //POST method for BookSeat
        {
            IEnumerable<Flight> objFlightList = _db.Flights;
            var flightFromDb = _db.Flights.Find(FlightID);
            if (Seats.Length > 0 && Seats != null)
            {
                int[] array = new int[Seats.Length];
                var SeatFromDb = _db.Seats.Where(x => x.Flight_num == FlightID).ToList();
                if (flightFromDb != null)
                for (int i = 0; i < Seats.Length; i++)
                {
                    array[i] = int.Parse(Seats[i]);
                    flightFromDb.BookedSeats += Seats[i];
                    flightFromDb.BookedSeats += ',';
                }
                
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] % 4 == 0)
                    {
                        SeatFromDb[i].Booked = true;
                    }
                    else if ((array[i] + 2) % 4 == 0)
                    {
                        SeatFromDb[array[i]].Booked = true;
                    }
                    else if (array[i] % 4 == 1)
                    {
                        SeatFromDb[array[i]].Booked = true;
                    }
                    else if ((array[i] + 2) % 4 == 1)
                    {
                        SeatFromDb[array[i]].Booked = true;
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("FlightList");
            }
            return RedirectToAction("FlightList");
        }
        public IActionResult AddFlight() //GET method for AddFlight
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFlight(Flight obj) //POST method for AddFlight
        {
            obj.Seats = obj.setSeats(obj.NumberOfSeats);
            if (ModelState.IsValid)
            {
                _db.Flights.Add(obj);
                for(int i = 0; i < obj.Seats.Count();i++)
                _db.Seats.Add(obj.Seats[i]);
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
