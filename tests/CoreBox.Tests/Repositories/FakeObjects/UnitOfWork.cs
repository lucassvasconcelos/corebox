using System;
using System.Threading.Tasks;
using CoreBox.Domain;
using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoreBox.Tests.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        public UnitOfWork(Context context)
            => _context = context;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
            => await transaction.CommitAsync();

        public async Task RollBackTransactionAsync(IDbContextTransaction transaction)
            => await transaction.RollbackAsync();

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity<TEntity>
            => typeof(TEntity) switch {
                Type tipo when tipo == typeof(Produto) => (IRepository<TEntity>)ProdutoRepository,
                _ => null
            };

        private IRepository<Produto> _produtoRepository;
        public IRepository<Produto> ProdutoRepository
        {
            get { return _produtoRepository ?? new ProdutoRepository(_context); }
            set { _produtoRepository = value; }
        }
    }
}