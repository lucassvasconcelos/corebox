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
            builder.Entity<Produto>()
                .HasKey(key => key.Id);

            builder.Entity<Produto>()
                .Property(prop => prop.DataCriacao)
                .IsRequired();

            builder.Entity<Produto>()
                .Property(prop => prop.DataUltimaAtualizacao)
                .IsRequired();

            builder.Entity<Produto>()
                .Property(prop => prop.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Entity<Produto>()
                .Property(prop => prop.Preco)
                .IsRequired();
        }
    }
}