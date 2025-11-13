using System.Diagnostics;
using Elastic.Clients.Elasticsearch;
using Elasticsearch.Net;
using Nest;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationContext db;
        private readonly ElasticsearchClient elastic;
        public HomeController(ApplicationContext db, ElasticsearchClient elastic)
        {
            this.db = db;
            this.elastic = elastic;
        }

        public IActionResult Main()
        {
            return View();
        }
    }
}
