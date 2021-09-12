using System.Threading.Tasks;
using CoreBox.Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreBox.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollBackTransactionAsync(IDbContextTransaction transaction);
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity<TEntity>;
    }
}