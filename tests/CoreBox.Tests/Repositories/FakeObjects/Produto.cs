using System;
using CoreBox.Domain;

namespace CoreBox.Tests.Repositories
{
    public class Produto : Entity<Produto>
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }

        private Produto() { }

        public static Produto Criar(string nome, decimal preco)
            => new Produto
            {
                Nome = nome,
                Preco = preco
            };

        public static Produto Carregar(Guid id, DateTime dataCriacao, DateTime dataUltimaAtualizacao, string nome, decimal preco)
            => new Produto
            {
                Id = id,
                Nome = nome,
                Preco = preco
            };

        public static void AtualizarPreco(Produto produto, decimal preco)
            => produto.Preco = preco;
    }
}