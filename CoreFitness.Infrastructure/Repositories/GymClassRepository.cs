using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreFitness.Infrastructure.Repositories
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext _context;

        public GymClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GymClass>> GetAllAsync()
            => await _context.GymClasses.ToListAsync();

        public async Task<GymClass?> GetByIdAsync(int id)
            => await _context.GymClasses.FindAsync(id);

        public async Task AddAsync(GymClass gymClass)
        {
            _context.GymClasses.Add(gymClass);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GymClass gymClass)
        {
            _context.GymClasses.Update(gymClass);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
                await _context.SaveChangesAsync();
            }
        }
    }
}