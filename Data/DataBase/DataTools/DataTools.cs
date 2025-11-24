using Microsoft.EntityFrameworkCore;
using TALENTSPHERE.Models;


namespace TALENTSPHERE.Data
{
    public class DataTools
    {
        public ApplicationContext db;

        public DataTools(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<long> GetUserByEmail(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("Failed to extract user in data base");
                return 0;
            }
            return user.Id;
        }
    }
}
