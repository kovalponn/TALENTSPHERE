using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Controllers
{
    public class AuthController : Controller
    {
        public ApplicationContext db;
        public AuthController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
