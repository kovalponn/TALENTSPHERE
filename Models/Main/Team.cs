namespace TALENTSPHERE.Models
{
    public class Team
    {
        public long Id { get; set; }
        public long ProjectId { get; set; }
        public long ChatId { get; set; }
        public string? Name { get; set; }
        public long[]? Participants { get; set; }
    }
}
