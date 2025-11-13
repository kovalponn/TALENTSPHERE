using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class Review
    {
        public int Id { get; set; }
        public GradeEnum Grade { get; set; }
        public string Description { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
