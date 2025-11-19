using System.Globalization;
using System.Linq;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        if (chat == null)
        {
            return;
        }

        // Проверяем, есть ли уже такой участник
        if (chat.Participants != null && chat.Participants.Contains(long.Parse(userId)))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await Clients.Group(chatId).SendAsync("ReceiveMessage", $"{userLogin} Присоединился к чату", "System");

            return;
        }

        // Инициализируем массив, если он null
        if (chat.Participants == null)
        {
            chat.Participants = new long[0];
        }

        // Создаем новый массив с добавленным участником
        var participantsList = chat.Participants.ToList();
        participantsList.Add(long.Parse(userId));
        chat.Participants = participantsList.ToArray();

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

        MessageBody messageBody = new MessageBody();
        messageBody.Text = message;
        messageBody.MediaUrl = "null";

        Message messageObject = new Message
        {
            OwnerId = long.Parse(userId),
            ChatId = long.Parse(chatId),
            View = Views.No,
            CreateTime = dateTime,
            Body = messageBody
        };

        await db.Messages.AddAsync(messageObject);
        await db.SaveChangesAsync();

        await Clients.Group(chatId).SendAsync("ReceiveMessage", message, userId, userLogin, dateTime.ToString());
    }
}