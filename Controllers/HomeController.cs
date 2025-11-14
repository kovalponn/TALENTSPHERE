using Nest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TALENTSPHERE.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationContext db;
        private IElasticClient elasticClient;
        public HomeController(ApplicationContext db, IElasticClient elasticClient)
        {
            this.db = db;
            this.elasticClient = elasticClient;
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new UserCreateViewModel
            {
                Roles = Enum.GetValues(typeof(UserRole))
                            .Cast<UserRole>()
                            .Select(r => new SelectListItem
                            {
                                Value = r.ToString(),
                                Text = r.ToString()
                            })
                            .ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Get(long id)
        {
            // Получить пользователя по id, включая все необходимые связанные данные, если нужно
            var user = await db.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    // Идентификатор — обычно базовая логика, его задавать не нужно
                    Login = model.Login,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    PhoneNumber = model.PhoneNumber,
                    Description = model.Description,
                    PhotoUrl = model.PhotoUrl,
                    CoverUrl = model.CoverUrl,
                    VideoCardUrl = model.VideoCardUrl,
                    Specialties = model.Specialties,
                    Role = model.Role,
                    CallAvailability = false, // Или по умолчанию
                    VideoChatConnect = 0 // Или по умолчанию
                };

                db.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index"); // или куда нужно после добавления
            }

            // Если модель не валидна, пересоздаем список ролей
            model.Roles = Enum.GetValues(typeof(UserRole))
                            .Cast<UserRole>()
                            .Select(r => new SelectListItem
                            {
                                Value = r.ToString(),
                                Text = r.ToString()
                            });
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Main()
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null) 
            {
                Console.WriteLine("Данные пользователя не найдены в User, возможно он не вошел в аккаунт");
                return View();
            }

            ShortUserViewModel userShort = new ShortUserViewModel(user);

            if (userShort == null)
            {
                Console.WriteLine("Данные пользователя не были сконвертированы для представления, возможно он не вошел в аккаунт");
                return View();
            }

            return View(userShort);
        }
    }
}
