namespace TALENTSPHERE.Models
{
    public class Message
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string? OwnerLogin { get; set; }
        public long ChatId { get; set; }
        public bool View { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Body { get; set; }
        public string? Content { get; set; }
    }
}
