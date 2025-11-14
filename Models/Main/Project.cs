using TALENTSPHERE.Models;
using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE
{
    public class Project
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
        public Directions[]? Direction { get; set; }
        public RequiredSpecialist[]? RequiredSpecialists { get; set; }
        public int BudgetRubble {  get; set; }
        public long[]? Responses { get; set; }
    }

    public class RequiredSpecialist
    {
        public string? NameDirection { get; set; }
        public int ShareRubble { get; set; }
        public int TermHour { get; set; }
    }
}
