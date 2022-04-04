using System.Linq.Expressions;
using CoreBox.Domain;

namespace CoreBox.Specification;

internal sealed class OrSpecification<TEntity> : Specification<TEntity> where TEntity : Entity<TEntity>
{
    private readonly Specification<TEntity> _left;
    private readonly Specification<TEntity> _right;

    public OrSpecification(Specification<TEntity> left, Specification<TEntity> right)
    {
        _left = left;
        _right = right;
    }

    public override Expression<Func<TEntity, bool>> ToExpression()
    {
        var leftExpression = _left.ToExpression();
        var rightExpression = _right.ToExpression();

        var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);
        var orExpression = Expression.OrElse(leftExpression.Body, invokedExpression);

        return (Expression<Func<TEntity, bool>>)Expression.Lambda(orExpression, leftExpression.Parameters);
    }
}