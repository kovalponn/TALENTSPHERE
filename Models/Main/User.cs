using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class User
    {
        public long Id { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        //public string Details { get; set; }
        public UserRole Role { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public string? CoverUrl { get; set; }
        public string? VideoCardUrl { get; set; }
        public Directions[]? Specialties { get; set; }
        public sbyte[]? Badges { get; set; }
        public long[]? Reviews { get; set; }
        public long[]? Posts { get; set; }
        public long[]? Subscribers { get; set; }
        public long[]? Subscriptions { get; set; }
        public long[]? FixServices { get; set; }
        public long[]? Projects { get; set; }
        public long[]? Teams { get; set; }
        public long[]? ChatsConnect { get; set; }
        public long VideoChatConnect { get; set; }
        public bool CallAvailability { get; set; }
    }
}
