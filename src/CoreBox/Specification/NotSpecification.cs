using System;
using System.Linq;
using System.Linq.Expressions;
using CoreBox.Domain;

namespace CoreBox.Specification
{
    internal sealed class NotSpecification<TEntity> : Specification<TEntity> where TEntity : Entity<TEntity>
    {
        private readonly Specification<TEntity> _specification;

        public NotSpecification(Specification<TEntity> specification)
            => _specification = specification;

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var expression = _specification.ToExpression();

            var notExpression = Expression.Not(expression.Body);
            return Expression.Lambda<Func<TEntity, bool>>(notExpression, expression.Parameters.Single());
        }
    }
}