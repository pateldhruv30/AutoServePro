using AutoServePro.Models;

namespace AutoServePro.ViewModels
{
    public class CustomerDashboardViewModel
    {
        public List<Appointment>? Appointments { get; set; }
        public List<Service>? Services { get; set; }
    }
}
