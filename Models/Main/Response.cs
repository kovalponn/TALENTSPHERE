using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class Response
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long ProjectId { get; set; }
        public string? Role {  get; set; }
        public int BudgetRubble { get; set; }
        public int TermHour { get; set; }
        public string? Description { get; set; }
        public OwnerResponses Ownerresponse { get; set; }
        public bool Ownerview { get; set; }
    }

    public enum OwnerResponses
    {
        Nothing, Yes, No
    }
}
