using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class Post
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public Directions? Direction { get; set; }
        public PostForms Form { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? MediaUrl { get; set; }
        public long[]? Comments { get; set; }
        public long Likes { get; set; }
        public int Reposts { get; set; }
        public long Views { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public enum PostForms
    {
        Article, Video
    }
}
