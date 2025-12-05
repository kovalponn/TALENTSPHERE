using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class Project
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectStatus Status { get; set; }
        public Directions? Direction { get; set; }
        public Durations? Duration { get; set; }
        public PaymentType PaymentType { get; set; }
        public RequiredSpecialists[]? RequiredSpecialist { get; set; }
        public decimal BudgetFrom { get; set; }
        public decimal BudgetTo { get; set; }
        public long[]? Responses { get; set; }
    }

    public class RequiredSpecialists
    {
        public long Id { get; set; }
        public string? Role { get; set; }
        public decimal ShareUsd { get; set; }
        public Durations Duration { get; set; }
    }

    public enum Durations
    {
        day1, days2, days3, days5, week1, days10, weeks2, weeks3, month1, month2, more_than_3_months
    }

    public enum PaymentType
    {
        Fix, Hour
    }

    public enum ProjectStatus
    {
        Close, Open
    }
}
