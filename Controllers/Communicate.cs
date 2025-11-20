using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [Authorize]
        public async Task<IActionResult> Chat(long chatId)
        {
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await db.Users.FirstOrDefaultAsync(e => e.Email == email);
            ViewData["IdUser"] = user?.Id;
            ViewData["LoginUser"] = user?.Login;
            ViewData["EmailUser"] = user?.Email;
            ViewData["IdChat"] = chatId;

            var chat = await db.Chats.Where(u => u.Id == chatId).FirstOrDefaultAsync();

            if (chat == null)
            {
                return View();
            }
            
            List<Message> messagesList = new List<Message>();

            long[]? messagesId = chat.Messages;

            if (messagesId == null)
            {
                return View(messagesList);
            }

            foreach (long i in messagesId)
            {
                Message? message = await db.Messages.FirstOrDefaultAsync(u => u.Id == i);
                if (message == null)
                {
                    continue;
                }
                messagesList.Add(message);
            }

            return View(messagesList);
        }

        public string CreateChat(string name)
        {
            Chat chat = new Chat
            {
                Name = name
            };

            db.Chats.Add(chat);

            db.SaveChanges();

            return $"Id: {chat.Id.ToString()} \n Name: {chat.Name}";
        }
        
        public string GetChat(long id)
        {
            var chat = db.Chats.Where(u => u.Id == id).FirstOrDefault();

            if (chat == null)
            {
                return "Chat not found";
            }

            return $"Id: {chat.Id.ToString()} \n Name: {chat.Name}";
        }

        public string DeleteChat(long id)
        {
            var chat = db.Chats.Where(u => u.Id == id).FirstOrDefault();

            if (chat == null)
            {
                return "Chat not found";
            }

            db.Remove(chat);
            db.SaveChanges();

            return $"Id: {chat.Id.ToString()} \n Name: {chat.Name} \n has been deleted";
        }

        public string EditNameChat(long id, string body)
        {
            var chat = db.Chats.Where(u => u.Id == id).FirstOrDefault();

            if (chat == null)
            {
                return "Chat not found";
            }

            chat.Name = body;
            db.SaveChanges();

            return $"Id: {chat.Id.ToString()} \n Name: {chat.Name} \n has been edit";
        }
    }
}
