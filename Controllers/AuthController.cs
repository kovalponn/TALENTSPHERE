using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TALENTSPHERE.Models;
using System;
using Microsoft.EntityFrameworkCore;

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
                return Redirect("Login");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Redirect("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("Login");
        }
    }
}
