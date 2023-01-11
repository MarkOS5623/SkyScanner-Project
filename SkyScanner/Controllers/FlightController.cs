using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.AspNetCore.Session;
using SkyScanner.Data;
using SkyScanner.Models;
using System;
using System.Linq;
using System.Net;
using System.Text;
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
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
            return View();
        }
        public IActionResult FlightList() //GET method for FlightList View
        {
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
            IEnumerable<Flight> objUserList = _db.Flights;
            return View(objUserList);
        }
        [HttpGet]
        public IActionResult Booking(string Seats) //GET method for Booking
        {
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
            if (Seats==null) return NotFound();
            List<string> list = Seats.Split(',').ToList(); 
            return View(list);
        }
        public IActionResult CancelBooking()
        {
            var BookedSeats = HttpContext.Session.GetString("Seats");
            var flightNum = HttpContext.Session.GetString("FlightID");
            var SeatArray = BookedSeats.Split(',');
            for (int i = 0; i < SeatArray.Count(); i++)
            {
                SeatArray[i] += "-"+flightNum;
            }
            for (int i = 0; i < SeatArray.Count(); i++)
            {
                var temp = _db.Seats.Find(SeatArray[i]);
                temp.Booked = false;
                _db.Seats.Update(temp);
            }
            var obj = _db.Flights.Find(flightNum);;
            var bookedIndexes = HttpContext.Session.GetString("Indexes");
            var IndexArray = bookedIndexes.Split(',');
            var ss = obj.BookedSeats;
            for (int i =0;i < IndexArray.Count();i++)
            if (obj.BookedSeats.Contains(IndexArray[i]))
            {
                    ss = ss.Replace(IndexArray[i]+",","");
            }
            obj.BookedSeats = ss;
            var haha = obj.BookedSeats;
            _db.Flights.Update(obj);
            _db.SaveChanges();
            HttpContext.Session.Clear();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Session");
            return RedirectToAction("Index","Home");
        }

        public IActionResult BookingEdit(string? flightid, string? BookingID)
        {
            if (flightid != null)
            {
                HttpContext.Session.SetString("More", flightid);
                HttpContext.Session.SetString("Booking", BookingID);
            }
            return Redirect("https://localhost:44313/Flight/BookSeat?flightid=4561");
        }
        public IActionResult BookSeat(string? flightid) //GET method for BookSeat View
        {
            HttpContext.Session.SetString("Seats", "start");
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
            if (flightid == null) return NotFound();
            var flightFromDb = _db.Flights.Find(flightid);
            if (flightFromDb == null) return NotFound();
            HttpContext.Session.SetString("FlightDate", flightFromDb.TakeOffDate.ToString());
            var SeatsFromDb = _db.Seats.Where(x => x.Flight_num == flightid && x.Booked == true).ToList();
            return View(flightFromDb);
        }
        [HttpPost]
        public IActionResult BookSeat(string[] Seats,string[] Indexes, string? FlightID) //POST method for BookSeat
        {
            //getting all the data we need in string form
            var Seatresult = new StringBuilder();
            foreach (var item in Seats)
            {
                Seatresult.Append(item.Remove(item.Length-5));
                Seatresult.Append(',');
            }
            var TEMP = Seatresult.ToString();
            TEMP = TEMP.Remove(TEMP.Length - 1);

            var Indexresult = new StringBuilder();
            foreach (var item in Indexes)
            {
                Indexresult.Append(item);
                Indexresult.Append(',');
            }
            var OBJ = Indexresult.ToString();
            OBJ = OBJ.Remove(OBJ.Length - 1);
            //Booking Session data Cookie setup
            HttpContext.Session.SetString("Indexes", OBJ);
            HttpContext.Session.SetString("Seats", TEMP);
            HttpContext.Session.SetString("FlightID", FlightID);
            var flightFromDb = _db.Flights.Find(FlightID); //finds the seat's flight
            HttpContext.Session.SetString("Origin", flightFromDb.Origin);
            HttpContext.Session.SetString("Destination", flightFromDb.Destination);
            double b = (flightFromDb.Price) * Seats.Length;
            var a = b.ToString();
            HttpContext.Session.SetString("Price", a);
            //Booking Session Cookie setup end
            int[] array = new int[Indexes.Length]; //int array for seat indexes
            if (flightFromDb != null) {
                for (int i = 0; i < Indexes.Length; i++)
                {
                    array[i] = int.Parse(Indexes[i]); //converting seat indexs to ints
                    flightFromDb.BookedSeats += Indexes[i]; //adding the booked seats to a string
                    flightFromDb.BookedSeats += ',';
                }
            };
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
                return RedirectToAction("Booking", Seats);
            }
            return RedirectToAction("BookSeat", flightFromDb.FlightId);
        }
        public IActionResult AddFlight() //GET method for AddFlight
        {
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFlight(Flight obj) //POST method for AddFlight
        {
            obj.Seats = obj.setSeats(obj.NumberOfSeats);
            if(_db.Flights.Find(obj.FlightId) != null) return View(obj);
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
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
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
            var Admin = Request.Cookies["AdminCookie"];
            ViewData["Admin"] = Admin;
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
