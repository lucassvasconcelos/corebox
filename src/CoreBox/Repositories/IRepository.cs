using CoreBox.Domain;
using CoreBox.Specification;

namespace CoreBox.Repositories;

public interface IRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task SaveAsync(TEntity entity);
    Task SaveRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> GetAsync(Specification<TEntity> specification);
    Task<IReadOnlyList<TEntity>> GetAllAsync(Specification<TEntity> specification = null);
}