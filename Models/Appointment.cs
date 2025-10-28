using System.ComponentModel.DataAnnotations;

namespace AutoServePro.Models
{
    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        InProgress,
        Completed,
        Cancelled
    }

    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        [Required]
        public string? VehicleName { get; set; }
        public Payment? Payment { get; set; }
    }
}
