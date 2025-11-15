using Microsoft.AspNetCore.Mvc;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationContext db;
        public AccountController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
