using System;
using System.Linq.Expressions;
using CoreBox.Specification;

namespace CoreBox.Tests.Repositories
{
    public class ProdutoComPrecoAbaixoSpecification : Specification<Produto>
    {
        private readonly decimal _valor;

        public ProdutoComPrecoAbaixoSpecification(decimal valor)
            => _valor = valor;

        public override Expression<Func<Produto, bool>> ToExpression()
            => produto => produto.Preco < _valor;
    }
}