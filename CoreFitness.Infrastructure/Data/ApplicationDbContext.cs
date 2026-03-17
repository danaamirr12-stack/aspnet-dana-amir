using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Membership> Memberships => Set<Membership>();
        public DbSet<GymClass> GymClasses => Set<GymClass>();
        public DbSet<Booking> Bookings => Set<Booking>();
    }
}