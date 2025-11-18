namespace TALENTSPHERE.Models
{
    public class Message
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long ChatId { get; set; }
        public Views View { get; set; }
        public DateTime CreateTime { get; set; }
        public MessageBody? Body { get; set; }
    }

    public class MessageBody
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public string MediaUrl { get; set; }
    }
}
