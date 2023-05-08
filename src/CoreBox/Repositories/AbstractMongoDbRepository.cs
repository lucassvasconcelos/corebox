using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CoreBox.Repositories;

public class AbstractMongoDbRepository<T> : IMongoDbRepository<T>
    where T : class
{
    private readonly string _connectionString;
    private readonly string _databaseName;
    private readonly string _collectionName;

    public AbstractMongoDbRepository(IConfiguration _configuration, string databaseName, string collectionName)
    {
        _connectionString = _configuration.GetConnectionString("MongoDbConnection");
        _databaseName = databaseName;
        _collectionName = collectionName;
    }

    public async Task AddAsync(T entity)
        => await GetCollection().InsertOneAsync(entity);

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        => await GetCollection().DeleteOneAsync(predicate);

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        => await GetCollection().Find(predicate).FirstOrDefaultAsync();

    public IMongoCollection<T> GetCollection()
        => GetOrCreateDatabase().GetCollection<T>(_collectionName);

    public IMongoDatabase GetOrCreateDatabase()
        => new MongoClient(_connectionString).GetDatabase(_databaseName);
}