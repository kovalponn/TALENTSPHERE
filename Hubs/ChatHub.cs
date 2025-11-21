using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Nest;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Hubs;
public class ChatHub : Hub
{
    public ApplicationContext db;

    public ChatHub(ApplicationContext db)
    {
        this.db = db;
    } 

    public async Task JoinToChat(string chatId, string userId, string userLogin)
    {
        var chat = await db.Chats.Where(u => u.Id == long.Parse(chatId)).FirstOrDefaultAsync();
        var user = await db.Users.Where(u => u.Id == long.Parse(userId)).FirstOrDefaultAsync();

        if (chat == null || user == null)
        {
            return;
        }

        // Проверяем, есть ли уже такой участник
        if (chat.Participants != null && chat.Participants.Contains(long.Parse(userId)))
        {
            if (user.ChatsConnect != null && user.ChatsConnect.Contains(long.Parse(chatId)))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
                return;
            }
            
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            return;
        }

        // Инициализируем массив, если он null
        if (chat.Participants == null)
        {
            chat.Participants = new long[0];
        }

        if (user.ChatsConnect == null)
        {
            user.ChatsConnect = new long[0];
        }

        // Создаем новый массив с добавленным участником
        var participantsList = chat.Participants.ToList();
        participantsList.Add(long.Parse(userId));
        chat.Participants = participantsList.ToArray();

        var userList = user.ChatsConnect.ToList();
        userList.Add(long.Parse(chatId));
        user.ChatsConnect = userList.ToArray();

        // Сохраняем изменения
        await db.SaveChangesAsync();

        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        await Clients.Group(chatId).SendAsync("ReceiveMessage", $"{userLogin} Вошел в чат", "", "System");
    }

    public async Task SendMessage(string chatId, string userId, string message, string userLogin, string dateString)
    {
        DateTime dateTime;
        if (DateTime.TryParse(dateString, null, DateTimeStyles.AdjustToUniversal, out dateTime))
        {
            // Теперь у вас есть DateTime
        }

        Message messageObject = new Message
        {
            OwnerId = long.Parse(userId),
            OwnerLogin = userLogin,
            ChatId = long.Parse(chatId),
            View = Views.No,
            CreateTime = dateTime,
            Body = message,
            Content = ""
        };

        var claimsPrincipal = Context.User;
        string? email = "";


        if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
        {
            email = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        }

        var chat = await db.Chats.FirstOrDefaultAsync(u => u.Id == long.Parse(chatId));

        if (chat != null)
        {
            await db.Messages.AddAsync(messageObject);
            await db.SaveChangesAsync();

            long newObject = messageObject.Id;

            long[] ? messageids = chat.Messages;
            long[]? newArray;

            if (messageids != null)
            {
                newArray = new long[messageids.Length + 1];
                Array.Copy(messageids, 0, newArray, 0, messageids.Length);
                newArray[newArray.Length - 1] = newObject;
            }
            else
            {
                newArray = new long[1];
                newArray[0] = newObject;
            }
            
            chat.Messages = newArray;

            await db.SaveChangesAsync();

            await Clients.Group(chatId).SendAsync("ReceiveMessage", message, userId, userLogin, dateTime.ToString(), email);
        }
    }
}