using System.ComponentModel.DataAnnotations;
using AutoServePro.Models;

namespace AutoServePro.ViewModels
{
    public class BookAppointmentViewModel
    {
        [Required]
        public int ServiceId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public string? PaymentMode { get; set; } // "Offline"

        [Required]
        public string? VehicleName { get; set; }

        public List<Service>? Services { get; set; }
    }
}
