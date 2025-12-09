using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel(User user, ShortProject shortProject1, ShortProject shortProject2, ShortProject shortProject3)
        {
            if (user == null)
            {
                this.Login = "Empty";
                this.Name = "Empty";
                this.Surname = "Empty";
                this.Email = "Empty";
                this.Role = UserRole.Empty;
                this.UsdBalance = 0;
                this.CountProject = 0;
                Console.WriteLine("Объект User оказался пустым");
            }
            else
            {
                this.Login = user.Login;
                this.Role = user.Role;
                this.UsdBalance = user.UsdBalance;
                if (user.Projects != null)
                {
                    this.CountProject = user.Projects.Length;
                }
                else
                {
                    this.CountProject = 0;
                }
                this.Name = user.Name;
                this.Surname = user.Surname;
                this.Email = user.Email;
                this.shortProject1 = shortProject1;
                this.shortProject2 = shortProject2;
                this.shortProject3 = shortProject3;
            }
        }
        public DashboardViewModel(User user)
        {
            if (user == null)
            {
                this.Login = "Empty";
                this.Name = "Empty";
                this.Surname = "Empty";
                this.Email = "Empty";
                this.Role = UserRole.Empty;
                this.UsdBalance = 0;
                this.CountProject = 0;
                Console.WriteLine("Объект User оказался пустым");
            }
            else
            {
                this.Login = user.Login;
                this.Role = user.Role;
                this.UsdBalance = user.UsdBalance;
                if (user.Projects != null)
                {
                    this.CountProject = user.Projects.Length;
                }
                else
                {
                    this.CountProject = 0;
                }
                this.Name = user.Name;
                this.Surname = user.Surname;
                this.Email = user.Email;

                ShortProject shortProject = new ShortProject();
                shortProject.Id = 0;

                this.shortProject1 = shortProject;
                this.shortProject2 = shortProject;
                this.shortProject3 = shortProject;
            }
        }

        public string? Login { get; set; }
        public UserRole Role { get; set; }
        public int CountProject {  get; set; }
        public decimal UsdBalance { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public ShortProject shortProject1 { get; set; }
        public ShortProject shortProject2 { get; set; }
        public ShortProject shortProject3 { get; set; }
    }

    public class ShortProject
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public Directions Direction { get; set; }
        public Durations Durations { get; set; }
        public ProjectStatus Status { get; set; }
        public int Responses { get; set; }
    }
}
