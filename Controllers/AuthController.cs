using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TALENTSPHERE.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TALENTSPHERE.Controllers
{
    public class AuthController : Controller
    {
        public readonly ApplicationContext db;
        public AuthController(ApplicationContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity == null)
            {
                Console.WriteLine("HttpContext.User.Identity == null !!!");
                return View();
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Пользователь авторизован");
                return RedirectToAction("Main", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await db.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
            Console.WriteLine($"Email: {email} \n Password: {password}");
            if (user == null)
            {
                Console.WriteLine("Не вошел");
                return Redirect("Login");
            }
                
            if (user.Email == null)
            {
                Console.WriteLine("Не вошел");
                return Redirect("Login");
            }
                

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Main", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.User.Identity == null)
            {
                Console.WriteLine("HttpContext.User.Identity == null !!!");
                return View();
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Пользователь авторизован");
                return RedirectToAction("Main", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string login, string email, string password)
        {
            User user = new User
            {
                Login = login,
                Email = email,
                Password = password,
                Name = "Обычный",
                Surname = "Пользователь"
            };

            var userCheckLogin = await db.Users.Where(u => u.Login == login).FirstOrDefaultAsync();

            if (userCheckLogin != null)
            {
                //ModelState.AddModelError("Login", "Already in use by another user");
                Console.WriteLine("Логин есть уже");
                return View();
            }
            
            db.Add(user);
            await db.SaveChangesAsync();
            Console.WriteLine("Успешная регистрация");
            return RedirectToAction("Login");
        }
    }
}
