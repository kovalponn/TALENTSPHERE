using System.Text.Json.Serialization;

namespace TALENTSPHERE.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
