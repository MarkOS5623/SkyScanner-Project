﻿using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkyScanner.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext _db;
        private IHttpContextAccessor _httpContextAccessor;
        public UserController(UserDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index(string? userid)
        {
            ViewData["userid"] = _db.Users.Find(userid).UserId.ToString();
            return View();
        }

        public IActionResult AddUser() //GET method for AddUser
        {
            return View();
        }
        
        public IActionResult Payment() //GET method for Payment
        {
            var userid = Request.Cookies["UserIdCookie"];
            var cardsFromDb = _db.CreditCards.Where(x => x.User_ID == userid).ToList();
            return View(cardsFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(User obj) //POST method for AddUser
        {
            Random rnd = new Random();
            obj.UserId = rnd.Next(1000, 9999).ToString();
            var check = _db.Users.Find(obj.UserId);
            while(check != null) //in case random userid is already used
            {
                obj.UserId = rnd.Next(1000, 9999).ToString();
                check = _db.Users.Find(obj.UserId);
            }
            ModelState.ClearValidationState("CreditCards");
            ModelState.MarkFieldValid("CreditCards");
            ModelState.ClearValidationState("Bookings");
            ModelState.MarkFieldValid("Bookings");
            ModelState.ClearValidationState("KeepLoggedIn");
            ModelState.MarkFieldValid("KeepLoggedIn");
            ModelState.ClearValidationState("Admin");
            ModelState.MarkFieldValid("Admin");
            if (ModelState.IsValid)
            {
                _db.Users.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(obj);
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated) return RedirectToAction("Index", "Home"); 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User obj)
        {
            var user = from a in _db.Users
                        where a.Email.Equals(obj.Email)
                        select a.Password;
            var id = from a in _db.Users
                       where a.Email.Equals(obj.Email)
                       select a;
            var temp = id.ToList();
            //Setting up a cookie that holds the UsersID
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";
            Response.Cookies.Append("UserIdCookie", temp[0].UserId, cookieOptions);
            //Setting up a cookie to see if User is admin or not
            var cookieOptions2 = new CookieOptions();
            cookieOptions2.Expires = DateTime.Now.AddDays(1);
            cookieOptions2.Path = "/";
            Response.Cookies.Append("AdminCookie", temp[0].Admin.ToString(), cookieOptions2);
            ViewData["Admin"] = temp[0].Admin.ToString();
            if (user == null || obj == null) return RedirectToAction("Login");
            else if(obj.Password == user.FirstOrDefault().ToString())
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, obj.Email)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties() { 
                    AllowRefresh = true, IsPersistent = obj.KeepLoggedIn
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home", new { userid = temp[0].UserId }); 
            }
            return RedirectToAction("Login");
        }
        public IActionResult UserList()
        {
            IEnumerable<User> objUserList = _db.Users;
            return View(objUserList);
        }
        
        public IActionResult MyBookings()
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            if (_db.Bookings.Count() > 0)
            {
                var BookingFromDb = from a in _db.Bookings
                                 where a.User_ID.Equals(userid)
                                 select a;
                if (BookingFromDb == null) return NotFound();
                return View(BookingFromDb);
            }
            return View();
        }
        public IActionResult PaymentMethods() 
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            if (_db.CreditCards.Count() > 0)
            {
                var CardFromDb = from a in _db.CreditCards
                                 where a.User_ID.Equals(userid)
                                 select a;
                if (CardFromDb == null) return NotFound();
                return View(CardFromDb);
            }
            return View();
        }
        public IActionResult AddCreditCard()
        {
            return View();
        }
        public IActionResult AddCreditCardPayment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCreditCardPayment(CreditCard obj) //POST method for AddFlight
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            obj.User_ID = userid;
            var temp = _db.Users.Find(userid);
            if (temp != null)
            {
                obj.User = temp;
            }
            ModelState.ClearValidationState("User");
            ModelState.MarkFieldValid("User");
            ModelState.ClearValidationState("User_ID");
            ModelState.MarkFieldValid("User_ID");
            if (ModelState.IsValid)
            {
                _db.CreditCards.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Home","Index");
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCreditCard(CreditCard obj) //POST method for AddFlight
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            obj.User_ID = userid;
            var temp = _db.Users.Find(userid);
            if (temp != null)
            {
                obj.User = temp;
            }
            ModelState.ClearValidationState("User");
            ModelState.MarkFieldValid("User");
            ModelState.ClearValidationState("User_ID");
            ModelState.MarkFieldValid("User_ID");
            if (ModelState.IsValid)
            {
                _db.CreditCards.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("PaymentMethods");
            }
            return View(obj);
        }

        public IActionResult AccountInfo() //GET method for AccountInfo View
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            var userFromDb = _db.Users.Find(userid);
            if (userFromDb == null) return NotFound();
            return View(userFromDb);
        }
    }
}
