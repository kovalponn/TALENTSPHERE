using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;
using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Controllers
{
    public class AccountController : Controller
    {
        public readonly ApplicationContext db;
        public AccountController(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Profile(long id)
        {
            if (id == 0)
            {
                string? email = User.FindFirst(ClaimTypes.Name)?.Value;

                var user = await db.Users.FirstOrDefaultAsync(e => e.Email == email);

                if (user == null)
                {
                    return BadRequest();
                }

                return View((user, user));
            }
            else
            {
                string? email = User.FindFirst(ClaimTypes.Name)?.Value;

                var ownerUser = await db.Users.FirstOrDefaultAsync(e => e.Email == email);

                var user = await db.Users.FirstOrDefaultAsync(e => e.Id == id);

                if (user == null || ownerUser == null)
                {
                    return BadRequest();
                }

                return View((user, ownerUser));
            }
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

                case "skills":
                    userUpdate.Speciality = inputValue;
                    break;
            }

            await db.SaveChangesAsync();

            return RedirectToAction("Settings", "Account");
        }

        [HttpGet]
        public IActionResult PreSettings()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Directions));
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PreSettings(string name, string surname, string description, Directions direction)
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await db.Users.FirstOrDefaultAsync(e => e.Email == email);

            if (user == null)
            {
                return RedirectToAction("Logout", "Auth");
            }

            user.Name = name;
            user.Surname = surname;
            user.Description = description;
            user.Direction = direction;

            await db.SaveChangesAsync();

            return RedirectToAction("Dashboard", "Home");
        }
    }
}
