using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetByUserIdAsync(string userId)
            => await _context.Bookings
                .Include(b => b.GymClass)
                .Where(b => b.UserId == userId)
                .ToListAsync();

        public async Task<bool> ExistsAsync(string userId, int gymClassId)
            => await _context.Bookings
                .AnyAsync(b => b.UserId == userId && b.GymClassId == gymClassId);

        public async Task AddAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Booking?> GetByUserAndClassAsync(string userId, int gymClassId)
            => await _context.Bookings
                .FirstOrDefaultAsync(b => b.UserId == userId && b.GymClassId == gymClassId);
    }
}