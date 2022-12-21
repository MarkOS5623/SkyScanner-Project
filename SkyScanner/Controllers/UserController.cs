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
        public UserController(UserDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
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
                return RedirectToAction("Index", "Home"); 
            }
            return RedirectToAction("Login");
        }
        public IActionResult UserList()
        {
            IEnumerable<User> objUserList = _db.Users;
            return View(objUserList);
        }
    }
}
