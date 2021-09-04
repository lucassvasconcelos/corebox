using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreBox.Repositories
{
    public interface IRepository<TEntity, TKey> : IDisposable where TEntity : Entity<TEntity, TKey>
    {
        Task SaveAsync(TEntity entity);
        Task SaveRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}