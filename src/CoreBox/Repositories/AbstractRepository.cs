using CoreBox.Domain;
using CoreBox.Specification;
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

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
        => await _entity.FindAsync(id);

    public virtual async Task<TEntity> GetAsync(Specification<TEntity> specification)
        => await _entity.AsQueryable().FirstOrDefaultAsync(specification.ToExpression());

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(Specification<TEntity> specification = null)
    {
        if (specification is null)
            return await _entity.AsQueryable().ToListAsync();

        return await _entity.Where(specification.ToExpression()).AsQueryable().ToListAsync();
    }
}