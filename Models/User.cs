using Microsoft.AspNetCore.Identity;

namespace AutoServePro.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public string? Role { get; set; } // "Customer" or "Admin"
        public ICollection<Vehicle>? Vehicles { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
