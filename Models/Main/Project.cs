using TALENTSPHERE.Models;

namespace TALENTSPHERE
{
    public class Project
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; }
        //public string Direction { get; set; }
        //public string RequiredSpecialists { get; set; }
        //public string Budget {  get; set; }
        //public string Responses { get; set; }
    }
}
