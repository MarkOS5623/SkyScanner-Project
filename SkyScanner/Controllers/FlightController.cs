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
        public IActionResult BookSeat(string[] Seats,string[] Indexes, string? FlightID) //POST method for BookSeat
        {
            IEnumerable<Flight> objFlightList = _db.Flights;
            var flightFromDb = _db.Flights.Find(FlightID);
            int[] array = new int[Indexes.Length];
            if (flightFromDb != null)
                for (int i = 0; i < Indexes.Length; i++)
                {
                    array[i] = int.Parse(Indexes[i]);
                    flightFromDb.BookedSeats += Indexes[i];
                    flightFromDb.BookedSeats += ',';
                }

            if (Seats.Length > 0 && Seats != null)
            {
                var SeatFromDb = _db.Seats.Where(x => x.Flight_num == FlightID).ToArray();
                for (int i = 0, j = 0; i < SeatFromDb.Count(); i++)
                {
                    var kmn = SeatFromDb[i].Seat_Num.Trim().Equals(Seats[j].Trim());
                    if (kmn)
                    {
                        var temp = _db.Seats.Find(Seats[j].Trim());
                        if (temp != null)
                        {
                            temp.Booked = true;
                            _db.Seats.Update(temp);
                        }
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
