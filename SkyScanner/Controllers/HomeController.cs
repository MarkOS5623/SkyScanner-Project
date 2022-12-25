using Microsoft.AspNetCore.Mvc;
using SkyScanner.Models;
using System.Diagnostics;
using SkyScanner.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using SkyScanner.Data;
using SkyScanner.Models;
namespace SkyScanner.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private FlightDbContext _db;
        public HomeController(ILogger<HomeController> logger, FlightDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            IEnumerable<Flight> objUserList = _db.Flights;
            return View(objUserList);
        }

        public async Task<IActionResult> LogOut()
        { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");
        }

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}