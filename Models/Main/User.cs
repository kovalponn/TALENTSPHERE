namespace TALENTSPHERE.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        //public string Details { get; set; }
        //public string Role { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string CoverUrl { get; set; }
        public string VideoCardUrl { get; set; }
        //public string Specialties { get; set; }
        //public string Badges { get; set; }
        //public string Reviews { get; set; }
        //public string Posts { get; set; }
        //public string Subscribers { get; set; }
        //public string Subscriptions { get; set; }
        //public string FixServices { get; set; }
        //public string Projects { get; set; }
        //public string Teams { get; set; }
        //public string ChatsConnect { get; set; }
        public long VideoChatConnect { get; set; }
        public bool CallAvailability { get; set; }
    }
}
