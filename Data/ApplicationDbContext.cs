using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoServePro.Models;

namespace AutoServePro.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Services
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, Name = "Oil Change", Description = "Complete oil change service", Price = 800.00M },
                new Service { Id = 2, Name = "Tire Rotation", Description = "Rotate tires for even wear", Price = 300.00M },
                new Service { Id = 3, Name = "Brake Inspection", Description = "Inspect and replace brakes if needed", Price = 1000.00M },
                new Service { Id = 4, Name = "Battery Check", Description = "Check battery health", Price = 200.00M },
                new Service { Id = 5, Name = "Engine Tune-Up", Description = "Complete engine performance check and tune-up", Price = 3000.00M },
                new Service { Id = 6, Name = "Air Filter Replacement", Description = "Replace cabin and engine air filters", Price = 500.00M },
                new Service { Id = 7, Name = "Transmission Service", Description = "Transmission fluid change and inspection", Price = 2500.00M },
                new Service { Id = 8, Name = "Cooling System Service", Description = "Radiator flush and coolant replacement", Price = 1200.00M },
                new Service { Id = 9, Name = "Wheel Alignment", Description = "Four-wheel alignment service", Price = 1000.00M },
                new Service { Id = 10, Name = "AC System Check", Description = "Air conditioning system inspection and recharge", Price = 800.00M }
            );

            // Seed admin user
            var adminId = "admin-id";
            var adminRoleId = "admin-role-id";
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = adminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            });
            var hasher = new PasswordHasher<User>();
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = adminId,
                UserName = "admin@autoservepro.com",
                NormalizedUserName = "ADMIN@AUTOSERVEPRO.COM",
                Email = "admin@autoservepro.com",
                NormalizedEmail = "ADMIN@AUTOSERVEPRO.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123!"),
                SecurityStamp = string.Empty,
                FullName = "System Admin",
                Role = "Admin"
            });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminId
            });
        }
    }
}
