using System.Linq.Expressions;
using MongoDB.Driver;

namespace CoreBox.Repositories;

public interface IMongoDbRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task DeleteAsync(Expression<Func<T, bool>> criteria);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria);
    IMongoCollection<T> GetCollection();
    IMongoDatabase GetOrCreateDatabase();
}