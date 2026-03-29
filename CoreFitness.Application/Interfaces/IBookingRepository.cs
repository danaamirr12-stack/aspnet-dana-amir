using CoreFitness.Domain.Entities;

namespace CoreFitness.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetByUserIdAsync(string userId);
        Task<bool> ExistsAsync(string userId, int gymClassId);
        Task AddAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<Booking?> GetByUserAndClassAsync(string userId, int gymClassId);
    }
}