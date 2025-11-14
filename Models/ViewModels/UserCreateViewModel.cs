using Microsoft.AspNetCore.Mvc.Rendering;
using TALENTSPHERE.Models.Common.Enums;

namespace TALENTSPHERE.Models
{
    public class UserCreateViewModel
    {
        public string? Login { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public string? CoverUrl { get; set; }
        public string? VideoCardUrl { get; set; }
        public Directions[]? Specialties { get; set; }
        public UserRole Role { get; set; }

        // Для выбора Role в форме
        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
