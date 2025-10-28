using System.ComponentModel.DataAnnotations;

namespace AutoServePro.Models
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed
    }

    public class Payment
    {
        public int Id { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Method { get; set; } // "Online" or "Offline"
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        public DateTime PaymentDate { get; set; } = DateTime.Now;

    }
}
