namespace TALENTSPHERE.Models
{
    public class Chat
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long[]? Participants { get; set; }
        public long[]? Messages { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
