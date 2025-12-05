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
                return RedirectToAction("Dashboard", "Home");
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
                ViewData["LoginMessage"] = "Неверный логин или пароль";                
                Console.WriteLine("Не вошел");
                return View();
            }
                
            if (user.Email == null)
            {
                ViewData["LoginMessage"] = "Неверный логин или пароль";
                Console.WriteLine("Не вошел");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) 
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            //var authProperties = new AuthenticationProperties
            //{
            //    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)
            //};

            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Dashboard", "Home");
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
                return RedirectToAction("Dashboard", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRole userRole, string login, string email, string password)
        {
            User user = new User
            {
                Login = login,
                Email = email,
                Password = password,
                UsdBalance = 0,
                Description = "-",
                Name = "Обычный",
                Surname = "Пользователь",
                Rating = 0,
                Grade = 0.0f,
                Role = userRole
            };

            //Console.WriteLine(userRole.ToString());

            var userCheckLogin = await db.Users.Where(u => u.Login == login).FirstOrDefaultAsync();

            bool x = false;
            bool y = false;

            if (userCheckLogin != null)
            {
                ViewData["RegisterMessage"] = "Такой логин занят";
                Console.WriteLine("Логин есть уже");
                x = true;
                y = true;
            }

            userCheckLogin = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (userCheckLogin != null)
            {
                ViewData["RegisterMessage"] = "Почта уже используется другим аккаунтом";
                Console.WriteLine("Почта есть уже");
                y = true;
            }
            
            if (x)
            {
                Console.WriteLine("Логин и пароль заняты");
                ViewData["RegisterMessage"] = "Почта или логин уже используются другим аккаунтом";
                return View();
            }

            if (y)
            {
                return View();
            }

            db.Add(user);
            await db.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(5)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            ViewBag.Login = user.Login;

            Console.WriteLine("Успешная регистрация");
            return RedirectToAction("PreSettings", "Account");
        }
    }
}
