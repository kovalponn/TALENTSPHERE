namespace TALENTSPHERE.Models
{
    public class ShortUserViewModel
    {
        public ShortUserViewModel(User user)
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
            }
        }

        public string? Login { get; set; }
        public UserRole Role { get; set; }
        public int CountProject {  get; set; }
        public decimal UsdBalance { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
