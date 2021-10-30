using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoreBox.Repositories.Cache
{
    public abstract class AbstractCache
    {
        protected readonly DbSet<CacheModel> _entity;
        protected readonly DbContext _dbContext;

        public AbstractCache(DbSet<CacheModel> entity, DbContext dbContext)
        {
            _entity = entity;
            _dbContext = dbContext;
        }

        public async Task SaveAsync<T>(string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key) || value is null)
                throw new ArgumentException("Não foi possível adicionar informação do cache: Chave ou Objeto não informado");

            await RemoveAsync(key);

            await SaveCacheModelAsync(key, value);
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Não foi possível remover informação do cache: Chave não informada");

            await RemoveCacheModelAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Não foi possível obter informação do cache: Chave não informada");

            var cacheModel = await GetCacheModelAsync(key);

            if (cacheModel is null)
                return default;

            return JsonSerializer.Deserialize<T>(cacheModel.Value);
        }

        protected virtual async Task SaveCacheModelAsync<T>(string key, T value)
        {
            var obj = JsonSerializer.Serialize<T>(value);
            await _entity.AddAsync(new CacheModel(key, obj));
            _dbContext.SaveChanges();
        }

        protected virtual async Task RemoveCacheModelAsync(string key)
        {
            var entity = await _entity.FindAsync(key);

            if (entity is null)
                return;

            _entity.Remove(entity);
            _dbContext.SaveChanges();
        }

        protected virtual async Task<CacheModel> GetCacheModelAsync(string key)
            => await _entity.FindAsync(key);
    }
}