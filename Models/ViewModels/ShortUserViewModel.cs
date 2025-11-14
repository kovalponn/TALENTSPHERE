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
                Console.WriteLine("Объект User оказался пустым");
            }
            
            this.Login = user.Login;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Email = user.Email;
        }

        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
