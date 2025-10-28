using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServePro.Data;
using AutoServePro.Models;

namespace AutoServePro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var users = await _context.Users.ToListAsync();
            var appointments = await _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Service)
                .ToListAsync();
            var services = await _context.Services.ToListAsync();

            var viewModel = new AdminDashboardViewModel
            {
                Users = users,
                Appointments = appointments,
                Services = services
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                ViewBag.SuccessMessage = "Service added successfully!";
                return View(new Service()); // Clear the form
            }
            ViewBag.ErrorMessage = "Failed to add service. Please check the form and try again.";
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, AppointmentStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                appointment.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatus status)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                payment.Status = status;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Dashboard");
        }
    }

    public class AdminDashboardViewModel
    {
        public List<User>? Users { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Service>? Services { get; set; }
    }
}
