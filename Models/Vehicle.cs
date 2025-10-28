using System.ComponentModel.DataAnnotations;

namespace AutoServePro.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public string? Make { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string? LicensePlate { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
