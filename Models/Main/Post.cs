namespace TALENTSPHERE.Models
{
    public class Post
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        //public string Direction { get; set; }
        public PostForms Form { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MediaUrl { get; set; }
        //public string Comments { get; set; }
        public long Likes { get; set; }
        //public string Reposts { get; set }
        public long Views { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
