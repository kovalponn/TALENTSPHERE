using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Hubs
{
    public class IsActiveHub : Hub
    {
        public ApplicationContext db;

        public IsActiveHub(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task TrackStatus(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == long.Parse(id));

            if (user == null)
            {
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, id);

            if (user.IsActive)
            {
                await Clients.Group(id.ToString()).SendAsync("ReceiveStatus", "true");
            }
            else
            {
                await Clients.Group(id.ToString()).SendAsync("ReceiveStatus", "false");
            }
        }

        public async Task ChangeStatus(string id, string newStatusString)
        {
            bool newStatus = true;

            if(newStatusString == "true")
            {
                newStatus = true;
            }
            else if (newStatusString == "false")
            {
                newStatus = false;
            }
            else
            {
                return;
            }

            long idLong = long.Parse(id);

            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == idLong);

            if (user == null)
            {
                return;
            }
         
            if (newStatus == user.IsActive)
            {
                return;
            }

            user.IsActive = newStatus;

            await db.SaveChangesAsync();

            if (newStatus)
            {
                await Clients.Group(id.ToString()).SendAsync("ReceiveStatus", "true");
                Console.WriteLine(user.Login + " активен");
            }
            else
            {
                await Clients.Group(id.ToString()).SendAsync("ReceiveStatus", "false");
                Console.WriteLine(user.Login + " неактивен");
            }
        }
    }
}
