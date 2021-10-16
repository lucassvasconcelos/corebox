using System;
using System.Linq.Expressions;
using CoreBox.Domain;

namespace CoreBox.Specification
{
    internal sealed class SpecificationBuilder<TEntity> : Specification<TEntity> where TEntity : Entity<TEntity>
    {
        public override Expression<Func<TEntity, bool>> ToExpression()
            => x => true;
    }
}