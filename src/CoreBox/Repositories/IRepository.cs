using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreBox.Domain;

namespace CoreBox.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        Task SaveAsync(TEntity entity);
        Task SaveRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
    }
}