using System.Linq.Expressions;
using CoreBox.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoreBox.Repositories;

public class AbstractRepository<TEntity> : IRepository<TEntity>
    where TEntity : Entity<TEntity>
{
    protected readonly DbSet<TEntity> _entity;
    protected readonly DbContext _dbContext;

    public AbstractRepository(DbSet<TEntity> entity, DbContext dbContext)
    {
        _entity = entity;
        _dbContext = dbContext;
    }

    public virtual async Task SaveAsync(TEntity entity)
        => await _entity.AddAsync(entity);

    public virtual async Task SaveRangeAsync(IEnumerable<TEntity> entities)
        => await _entity.AddRangeAsync(entities);

    public virtual Task UpdateAsync(TEntity entity)
    {
        _entity.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _entity.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        _entity.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entity.AsQueryable().FirstOrDefaultAsync(predicate);

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, int? skip = null, int? take = null, Expression<Func<TEntity, object>>? orderBy = null, bool orderByDescending = false, bool noTracking = false)
    {
        var query = _entity.AsQueryable();

        if (predicate != null) query = query.Where(predicate);
        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);
        if (noTracking) query = query.AsNoTracking();
        
        if (orderBy != null)
        {
            if (orderByDescending) query = query.OrderByDescending(orderBy);
            else query = query.OrderBy(orderBy);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        => await _entity.AsQueryable().AnyAsync(predicate);

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate is null)
            return await _entity.AsQueryable().CountAsync();

        return await _entity.AsQueryable().CountAsync(predicate);
    }
}