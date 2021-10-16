using System;
using System.Linq.Expressions;
using CoreBox.Specification;

namespace CoreBox.Tests.Specification
{
    public class PessoaIdosaSpecification : Specification<Pessoa>
    {
        public override Expression<Func<Pessoa, bool>> ToExpression()
            => pessoa => pessoa.Idade > 60;
    }
}