using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoServePro.Data;
using AutoServePro.Models;
using AutoServePro.ViewModels;

namespace AutoServePro.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public CustomerController(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointments = await _context.Appointments
                .Include(a => a.Service)
                .Where(a => a.UserId == user.Id)
                .ToListAsync();

            var services = await _context.Services.ToListAsync();

            var viewModel = new CustomerDashboardViewModel
            {
                Appointments = appointments,
                Services = services
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> BookAppointment()
        {
            var services = await _context.Services.ToListAsync();

            var viewModel = new BookAppointmentViewModel
            {
                Services = services
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAppointment(BookAppointmentViewModel model)
        {
            // Always reload services for the dropdown if we return the view on failure
            var services = await _context.Services.ToListAsync();
            model.Services = services;

            // --- IMPROVED: Display Validation/Model Binding Errors Directly ---
            if (!ModelState.IsValid)
            {
                // This block is executed if ServiceId is an empty string, causing a binding failure.
                // We check for the error that is not tied to a specific field key.
                if (ModelState.ContainsKey("ServiceId") && ModelState["ServiceId"].Errors.Any() && ModelState["ServiceId"].Errors.First().ErrorMessage.Contains("invalid"))
                {
                    ModelState.AddModelError("", "Please ensure a valid Service is selected from the dropdown.");
                }
                
                ViewBag.ErrorMessage = "Failed to book appointment. Please check all fields.";
                return View(model);
            }

            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    ModelState.AddModelError("", "User not found. Please log in again.");
                    ViewBag.ErrorMessage = "Failed to book appointment.";
                    return View(model);
                }

                // 1. Create and Save Appointment
                var appointment = new Appointment
                {
                    UserId = user.Id,
                    ServiceId = model.ServiceId,
                    AppointmentDate = model.AppointmentDate,
                    VehicleName = model.VehicleName
                };

                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync(); // First Save (Appointment)

                // 2. Create and Save Payment
                var service = await _context.Services.FindAsync(model.ServiceId);
                if (service == null)
                {
                    ModelState.AddModelError("", "Selected service not found.");
                    return View(model);
                }

                var payment = new Models.Payment
                {
                    AppointmentId = appointment.Id,
                    Amount = service.Price,
                    Method = model.PaymentMode,
                    Status = PaymentStatus.Completed
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync(); // Second Save (Payment)



                ViewBag.SuccessMessage = "Appointment booked successfully!";
                // Return a new form
                return View(new BookAppointmentViewModel { Services = services });
            }
            catch (DbUpdateException dbEx) // Catch specific database exceptions (Foreign Key, etc.)
            {
                // --- CRITICAL DEBUGGING STEP: Show the actual database error ---
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                ModelState.AddModelError("", $"DATABASE ERROR: Failed to save data. Details: {innerMessage}. Check if ServiceId or UserId exist in the database.");
            }
            catch (Exception ex)
            {
                // Generic catch-all for other runtime errors
                ModelState.AddModelError("", $"An unexpected runtime error occurred: {ex.Message}");
            }

            // This line executes if any error (validation or exception) occurred.
            ViewBag.ErrorMessage = "Failed to book appointment. Please check the form and try again.";
            return View(model);
        }

        public async Task<IActionResult> ServiceHistory()
        {
            var user = await _userManager.GetUserAsync(User);
            var appointments = await _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.Payment)
                .Where(a => a.UserId == user.Id)
                .ToListAsync();

            return View(appointments);
        }
    }
}