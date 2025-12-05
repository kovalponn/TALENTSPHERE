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
                    project.BudgetFrom = model.BudgetFrom;
                    project.BudgetTo = model.BudgetTo;
                    project.Status = ProjectStatus.Open;
                    project.Direction = model.Category;
                    project.Duration = model.Duration;
                    project.PaymentType = model.PaymentType;
                    project.OwnerId = long.Parse(userId);

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

        //[HttpPost]
        //public IActionResult CreateProject(ProjectCreateViewModel model)
        //{
        //    if (model != null)
        //    {
        //        Console.WriteLine(model.Name);
        //        Console.WriteLine(model.Description);
        //        Console.WriteLine(model.BudgetFrom);
        //        Console.WriteLine(model.BudgetTo);
        //        Console.WriteLine(model.Category.ToString());
        //        Console.WriteLine(model.PaymentType.ToString());
        //        Console.WriteLine(model.Duration.ToString());

        //        if (model.RequiredSpecialist != null)
        //        {
        //            int x = 0;
        //            foreach (var i in model.RequiredSpecialist)
        //            {
        //                x++;
        //                if (i.Role == null)
        //                {
        //                    continue;
        //                }
        //                Console.WriteLine($"Специалист {x.ToString()} \n" +
        //                    $"{i.Role.ToString()} \n {i.ShareUsd.ToString()} \n {i.Duration.ToString()}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Нет специалистов");
        //        }

        //        return RedirectToAction("CreateProject");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Входные данные пусты");
        //        return View();
        //    }
        //}
    }
}
