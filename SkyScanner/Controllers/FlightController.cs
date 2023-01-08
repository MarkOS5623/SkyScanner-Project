using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;
using System.Web.Optimization;

namespace SkyScanner.Controllers
{
    public class FlightController : Controller
    {
        private FlightDbContext _db;
        private IHttpContextAccessor _httpContextAccessor;
        public static bool EnableOptimizations { get; set; }
        public FlightController(FlightDbContext db, IHttpContextAccessor httpContextAccessor) //setting up db context
        {
            _db = db;
            _db.Seats = db.Seats;
            this._httpContextAccessor = httpContextAccessor;
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
            var flightFromDb = _db.Flights.Find(FlightID); //finds the seat's flight
            int[] array = new int[Indexes.Length]; //int array for seat indexes
            if (flightFromDb != null)
                for (int i = 0; i < Indexes.Length; i++)
                {
                    array[i] = int.Parse(Indexes[i]); //converting seat indexs to ints
                    flightFromDb.BookedSeats += Indexes[i]; //adding the booked seats to a string
                    flightFromDb.BookedSeats += ',';
                }
            if (Seats.Length > 0 && Seats != null)
            {
                var SeatFromDb = _db.Seats.Where(x => x.Flight_num == FlightID).ToArray(); //gets all the seats from the flight
                for (int i = 0, j = 0; i < SeatFromDb.Count() && j < Seats.Length; i++)
                {
                    var Temp = SeatFromDb[i].Seat_Num.Trim().Equals(Seats[j].Trim()); //checks if the current seats is the one we are looking for
                    if (Temp)
                    {
                        var temp = _db.Seats.Find(Seats[j].Trim()); //gets the seat
                        if (temp != null)
                        {
                            temp.Booked = true; //marks the seat as booked
                            _db.Seats.Update(temp);
                            j++;
                        }
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Booking", "User");
            }
            return RedirectToAction("BookSeat", flightFromDb.FlightId);
        }
        public IActionResult AddFlight() //GET method for AddFlight
        {
            return View();
        }

        public IActionResult Booking() //GET method for AddFlight
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
