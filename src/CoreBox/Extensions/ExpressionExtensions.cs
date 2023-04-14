using System.Linq.Expressions;

namespace CoreBox.Extensions;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> True<T>()
        => e => true;

    public static Expression<Func<T, bool>> False<T>()
        => e => false;

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        => Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>())), expr1.Parameters);

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        => Expression.Lambda<Func<T, bool>>(Expression.OrElse(expr1.Body, Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>())), expr1.Parameters);
}