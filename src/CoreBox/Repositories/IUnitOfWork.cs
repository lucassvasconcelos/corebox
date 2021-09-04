using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreBox.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction transaction);
        Task RollBackTransactionAsync(IDbContextTransaction transaction);
    }
}