using Microsoft.AspNetCore.Mvc;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Controllers
{
    public class Communicate : Controller
    {
        public ApplicationContext db;

        public Communicate(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Chat()
        {
            return View();
        }

        public string CreateChat(string name)
        {
            Chat chat = new Chat
            {
                Name = name
            };

            db.Chats.Add(chat);

            db.SaveChanges();

            return chat.Id.ToString();
        }
    }
}
