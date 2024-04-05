using System.Linq.Expressions;
using CoreBox.Domain;

namespace CoreBox.Repositories;

public interface IRepository<TEntity> where TEntity : Entity<TEntity>
{
    Task SaveAsync(TEntity entity);
    Task SaveRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task DeleteRangeAsync(IEnumerable<TEntity> entities);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null, int? skip = null, int? take = null, Expression<Func<TEntity, object>> orderBy = null, bool orderByDescending = false, bool noTracking = false);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
}