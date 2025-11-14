namespace TALENTSPHERE.Models
{
    public class FixService
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? MediaUrl { get; set; }
        public int PriceRubble { get; set; }
        public int TermHour { get; set; }
        public long Views { get; set; }
        public int Sold { get; set; }
        public long[]? AdditionalService { get; set; }
    }
}
