using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreBox.Repositories
{
    public class AbstractRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : Entity<TEntity, TKey>
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

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
            => await _entity.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _entity.ToListAsync();

        public void Dispose()
            => _dbContext.Dispose();
    }
}