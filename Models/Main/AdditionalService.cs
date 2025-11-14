namespace TALENTSPHERE.Models
{
    public class AdditionalService
    {
        public long Id { get; set; }
        public long MainServiceId { get; set; }
        public string? Description { get; set; }
        public int PriceRubble { get; set; }
        public int TermHour { get; set; }
    }
}
