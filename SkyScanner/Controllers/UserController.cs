using Microsoft.AspNetCore.Mvc;
using SkyScanner.Data;
using SkyScanner.Models;

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
        public IActionResult UserList()
        {
            IEnumerable<User> objUserList = _db.Users;
            return View(objUserList);
        }
    }
}
