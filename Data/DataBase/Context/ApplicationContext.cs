using Microsoft.EntityFrameworkCore;

namespace TALENTSPHERE.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        public DbSet<FixService> FixServices { get; set; } = null!;
        public DbSet<AdditionalService> AdditionalServices { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Response> Responses { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Chat> Chats { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<VideoCall> VideoCalls { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated(); 
        }
    }
}
