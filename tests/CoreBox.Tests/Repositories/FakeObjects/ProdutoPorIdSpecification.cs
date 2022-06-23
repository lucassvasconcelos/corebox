using System;
using System.Linq.Expressions;
using CoreBox.Specification;

namespace CoreBox.Tests.Repositories
{
    public class ProdutoPorIdSpecification : Specification<Produto>
    {
        private readonly Guid _id;

        public ProdutoPorIdSpecification(Guid id)
            => _id = id;

        public override Expression<Func<Produto, bool>> ToExpression()
            => produto => produto.Id == _id;
    }
}