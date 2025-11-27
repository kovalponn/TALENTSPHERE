using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class Review
    {
        public int Id { get; set; }
        public GradeEnum Grade { get; set; }
        public string? Description { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public enum GradeEnum
    {
        One = 1, Two = 2, Three = 3, Four = 4, Five = 5
    }
}
