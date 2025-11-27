using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(ProjectCreateViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    return RedirectToAction("CreateProject");
            //}

            //ViewBag.Categories = Enum.GetValues(typeof(Directions));
            //return View(model);
            if (model != null)
            {
                Console.WriteLine(model.Name);
                Console.WriteLine(model.Description);
                Console.WriteLine(model.BudgetFrom);
                Console.WriteLine(model.BudgetTo);
                Console.WriteLine(model.Category.ToString());
                Console.WriteLine(model.PaymentType.ToString());
                Console.WriteLine(model.Duration.ToString());
                
                if (model.RequiredSpecialist != null)
                {
                    int x = 0;
                    foreach (var i in model.RequiredSpecialist)
                    {
                        x++;
                        if (i.Role == null)
                        {
                            continue;
                        }
                        Console.WriteLine($"Специалист {x.ToString()} \n" +
                            $"{i.Role.ToString()} \n {i.ShareUsd.ToString()} \n {i.Duration.ToString()}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет специалистов");
                }

                return RedirectToAction("CreateProject");
            }
            else
            {
                Console.WriteLine("Входные данные пусты");
                return View();
            }
        }

    }
}
