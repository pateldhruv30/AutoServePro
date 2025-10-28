using System.ComponentModel.DataAnnotations;

namespace AutoServePro.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
