using CoreBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoreBox.Tests.Repositories
{
    public class Context : DbContext, IDbContext<Context>
    {
        public DbSet<Produto> Produtos { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Produto>().HasKey(key => key.Id);
            builder.Entity<Produto>().Property(prop => prop.Nome).HasColumnType("VARCHAR(50)");
            builder.Entity<Produto>().Property(prop => prop.Preco);
            builder.Entity<Produto>().Property(prop => prop.DataCriacao);
            builder.Entity<Produto>().Property(prop => prop.IdUsuarioCriacao);
            builder.Entity<Produto>().Property(prop => prop.DataUltimaAtualizacao);
            builder.Entity<Produto>().Property(prop => prop.IdUsuarioAtualizacao);
            builder.Entity<Produto>().Property(prop => prop.DataExclusao);
            builder.Entity<Produto>().Property(prop => prop.IdUsuarioExclusao);
        }
    }
}