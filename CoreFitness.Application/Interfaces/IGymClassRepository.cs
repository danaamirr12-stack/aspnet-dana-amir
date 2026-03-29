using System;
using CoreFitness.Domain.Entities;

namespace CoreFitness.Application.Interfaces
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAllAsync();
        Task<GymClass?> GetByIdAsync(int id);
        Task AddAsync(GymClass gymClass);
        Task UpdateAsync(GymClass gymClass);
        Task DeleteAsync(int id);
    }
}