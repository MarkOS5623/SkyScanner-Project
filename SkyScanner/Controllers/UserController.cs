using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

        public IActionResult AddUser() //GET method for AddFlight
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(User obj) //POST method for AddFlight
        {
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
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(1);
            cookieOptions.Path = "/";
            Response.Cookies.Append("UserIdCookie", temp[0].UserId, cookieOptions);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PaymentMethods(string cardnum, int expM, int expY, int CVV) //POST method for AddFlight
        {
            var userid = Request.Cookies["UserIdCookie"];
            if (userid == null) return NotFound();
            var userFromDb = _db.Users.Find(userid);
            var temp = new CreditCard(userid, userFromDb, cardnum, expM, expY, CVV);
            if (ModelState.IsValid)
            {
                _db.CreditCards.Add(temp);
                _db.SaveChanges();
                return RedirectToAction("PaymentMethods");
            }
            return View();
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
