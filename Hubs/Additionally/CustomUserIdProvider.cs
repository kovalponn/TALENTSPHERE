using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Data;
using TALENTSPHERE.Models;

namespace TALENTSPHERE.Hubs.Additionally
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            var httpContext = connection.GetHttpContext();
            if (httpContext == null)
            {
                Console.WriteLine("HttpContext are not find");
                return "";
            }
            
            string? id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (id != null)
            {
                return id;
            }

            Console.WriteLine("User Id are not find in Cookie");
            return "User Id are not find in Cookie";
        }
    }
}
