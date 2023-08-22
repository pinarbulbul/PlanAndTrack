using System;
using PlanAndTrack.Domain.Entities;

namespace PlanAndTrack.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T?> DeleteAsync(int id);
        Task DeleteAsync(T entity);
    }
}

