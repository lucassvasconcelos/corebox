using System.Linq.Expressions;
using CoreBox.Domain;

namespace CoreBox.Specification;

public abstract class Specification<TEntity> where TEntity : Entity<TEntity>
{
    public abstract Expression<Func<TEntity, bool>> ToExpression();

    public Specification<TEntity> And(Specification<TEntity> specification)
    {
        if (this == All) return specification;
        if (specification == All) return this;

        return new AndSpecification<TEntity>(this, specification);
    }

    public Specification<TEntity> Or(Specification<TEntity> specification)
    {
        if (this == All || specification == All) return All;

        return new OrSpecification<TEntity>(this, specification);
    }

    public Specification<TEntity> Not(Specification<TEntity> specification = null)
        => new NotSpecification<TEntity>(this);

    public static readonly Specification<TEntity> All = new SpecificationBuilder<TEntity>();

    public bool IsSatisfiedBy(TEntity entity)
    {
        Func<TEntity, bool> predicate = ToExpression().Compile();
        return predicate(entity);
    }
}