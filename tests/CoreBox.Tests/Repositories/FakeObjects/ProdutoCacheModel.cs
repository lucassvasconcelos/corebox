using System;

namespace CoreBox.Tests.Repositories.FakeObjects
{
    public class ProdutoCacheModel
    {
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public static Produto ToEntity(ProdutoCacheModel model)
            => Produto.Carregar(model.Id, model.DataCriacao, model.DataUltimaAtualizacao, model.Nome, model.Preco);
    }
}