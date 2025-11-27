using System;
using System.Security.Claims;
using System.Text.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TALENTSPHERE.Controllers
{
    public record class ChatsList 
    {
        public long Id { get; set; }
        public string? Name { get; set; }
    }

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

            if (user == null)
            {
                return View();
            }

            List<Message> messagesList = new List<Message>();

            if (chat != null)
            {
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
            }

            List<ChatsList> chatsLists = new List<ChatsList>();

            if (user.ChatsConnect != null)
            {
                foreach (var i in user.ChatsConnect)
                {
                    var chatForView = await db.Chats.FirstOrDefaultAsync(u => u.Id == i);
                    if (chatForView == null || chatForView.Name == null)
                    {
                        continue;
                    }
                    ChatsList chatsList = new ChatsList();
                    chatsList.Id = chatForView.Id;
                    chatsList.Name = chatForView.Name;
                    chatsLists.Add(chatsList);
                }
            }
            if (user.ChatsConnect == null)
            {
                Console.WriteLine("Нет чатов");
            }
            else
            {
                foreach (var i in user.ChatsConnect)
                {
                    Console.WriteLine(i);
                }
            }

            ViewBag.ChatList = chatsLists.ToArray();
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

        public async Task<string> DeleteChat(long id)
        {
            var chat = db.Chats.Where(u => u.Id == id).FirstOrDefault();
            string? email = User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await db.Users.FirstOrDefaultAsync(e => e.Email == email);

            if (chat == null)
            {
                return "Chat not found";
            }

            if (user == null)
            {
                return "You user is not found";
            }

            long[]? messages = user.ChatsConnect;

            if (messages != null)
            {
                List<long> messagesList = new List<long>(messages);

                messagesList.Remove(id);

                messages = messagesList.ToArray();

                user.ChatsConnect = messages;
            } 

            db.Remove(chat);
            await db.SaveChangesAsync();

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

        public IActionResult ChatList()
        {
            return View();
        }
    }
}
