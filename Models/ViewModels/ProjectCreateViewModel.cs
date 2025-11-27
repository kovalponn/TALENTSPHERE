using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models.ViewModels
{
    public class ProjectCreateViewModel
    {
        //RequiredSpecialists requiredSpecialists = new RequiredSpecialists { Duration = Durations.day1, Role = "", ShareUsd = 0 };
        public string? Name { get; set; }
        public Directions Category { get; set; }
        public string? Description { get; set; }
        public string? Skills { get; set; }
        public RequiredSpecialists[]? RequiredSpecialist { get; set; }
        public Durations Duration { get; set; }
        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
