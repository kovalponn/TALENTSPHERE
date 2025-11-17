using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Profile()
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(string FieldName, string inputValue)
        {
            var fieldIdentifier = FieldName; 
            var value = inputValue;
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            var userUpdate = await db.Users.FirstOrDefaultAsync(e => e.Email == email);

            if (userUpdate == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            if (inputValue == null)
            {
                Console.WriteLine("Input value == null");
                return RedirectToAction("Settings", "Account");                
            }

            switch (fieldIdentifier)
            {
                case "name":
                    userUpdate.Name = inputValue;
                    break;

                case "surname":
                    userUpdate.Surname = inputValue;
                    break;

                case "login":
                    bool userExists = db.Users.Any(u => u.Login == inputValue);

                    if (userExists)
                    {
                        ViewData["LoginMessage"] = "! Логин занят !";
                        Console.WriteLine("Логин занят");
                        return RedirectToAction("Settings", "Account"); 
                    }
                    else
                    {
                        userUpdate.Login = inputValue;
                    }
                    break;
                case "description":
                    userUpdate.Description = inputValue;
                    break;
            }

            await db.SaveChangesAsync();

            return RedirectToAction("Settings", "Account");
        }
    }
}
