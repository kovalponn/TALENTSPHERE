using System.Security.Claims;
using Elastic.Clients.Elasticsearch.Aggregations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nest;
using TALENTSPHERE.Models;
using TALENTSPHERE.Models.Common.Enums;
using TALENTSPHERE.Models.ViewModels;

namespace TALENTSPHERE.Controllers
{
    public class ProjectController : Controller
    {
        public ApplicationContext db;
        private IElasticClient elasticClient;
        public ProjectController(ApplicationContext db, IElasticClient elasticClient)
        {
            this.db = db;
            this.elasticClient = elasticClient;
        }

        public IActionResult CreateProject()
        {
            ViewBag.Categories = Enum.GetValues(typeof(Directions));
            ViewBag.Errors = "";
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreateViewModel model)
        {
            if (model != null)
            {
                string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return NotFound();
                }

                var user = await db.Users.FindAsync(long.Parse(userId));

                if (model.RequiredSpecialist != null && user != null)
                {
                    Project project = new Project();
                    project.Name = model.Name;
                    project.Description = model.Description;
                    project.Skills = model.Skills;
                    project.BudgetFrom = model.BudgetFrom;
                    project.BudgetTo = model.BudgetTo;
                    project.Status = ProjectStatus.Open;
                    project.Direction = model.Category;
                    project.Duration = model.Duration;
                    project.PaymentType = model.PaymentType;
                    project.OwnerId = long.Parse(userId);
                    project.ProgressProcent = 0;
                    project.CreateDate = DateTime.Now.ToUniversalTime();

                    await db.Projects.AddAsync(project);
                    await db.SaveChangesAsync();

                    if (user.Projects != null)
                    {
                        List<long> projectsId = user.Projects.ToList();
                        projectsId.Add(project.Id);
                        user.Projects = projectsId.ToArray();
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        long[] projectsId = new long[1];
                        projectsId[0] = project.Id;
                        user.Projects = projectsId;
                        await db.SaveChangesAsync();
                    }

                    Console.WriteLine($"Все Ок \n ID in project: {project.Id}");
                    foreach (var i in user.Projects)
                    {
                        Console.WriteLine(i);
                    }
                    return RedirectToAction("Dashboard", "Home");
                }
                else
                {
                    Console.WriteLine("Входные данные вероятно не полные");
                    return View();
                }
            }
            else
            {
                Console.WriteLine("Входные данные пусты");
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> ProjectList()
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;

            var user = await db.Users.Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                Console.WriteLine("Данные пользователя не найдены в User, возможно он не вошел в аккаунт");
                return View();
            }

            List<Project> Projects = db.Projects.Where(u => u.OwnerId == user.Id).ToList();

            List<ShortProject> shortProjects = new List<ShortProject>();

            for (int i = Projects.Count - 1; i >= 0; i--)
            {
                var project = Projects[i];
                int responses = project.Responses != null ? project.Responses.Length : 0;

                shortProjects.Add(new ShortProject
                {
                    Id = project.Id,
                    Name = project.Name,
                    Direction = project.Direction,
                    Durations = project.Duration,
                    Status = project.Status,
                    Responses = responses,
                    ProgressProcent = project.ProgressProcent
                });
            }

            return View(shortProjects);
        }

        public async Task<IActionResult> ViewProject(long id)
        {
            var project = await db.Projects.FirstOrDefaultAsync(u => u.Id == id);

            if (project == null)
            {
                return View();
            }

            DateTime currentTime = DateTime.Now.ToUniversalTime();

            TimeSpan difference = currentTime.Subtract(project.CreateDate);

            double hourBack = Math.Ceiling(difference.TotalHours);

            if (hourBack < 24)
            {
                ViewData["hourBack"] = $"{hourBack} ч. назад";
            }
            if (hourBack > 24)
            {
                hourBack = hourBack / 24;
                hourBack = Math.Ceiling(hourBack);

                ViewData["hourBack"] = $"{hourBack} дн. назад";
            }
            if (hourBack == 24)
            {
                ViewData["hourBack"] = $"{hourBack} день назад";
            }

            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == project.OwnerId);

            if (user == null) 
            { 
                return View();
            }
            
            if (project.Responses != null)
            {
                ViewData["response"] = project.Responses.Length;
            }
            else
            {
                ViewData["response"] = 0;
            }

            ViewData["userRaiting"] = user.Grade;

            return View((project ,user));
        }
    }
}
