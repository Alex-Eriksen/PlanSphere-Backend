using System.Linq.Expressions;

namespace PlanSphere.Core.Extensions;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByExpression<T, U>(this IQueryable<T> source, Expression<Func<T, U>> keySelector, bool descending = true)
    {
        var method = descending ? "OrderByDescending" : "OrderBy";

        var methodCall = Expression.Call(
            typeof(Queryable),
            method,
            [typeof(T), typeof(U)],
            source.Expression,
            Expression.Quote(keySelector)
        );

        return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
    }
    
    public static IOrderedQueryable<T> ThenByExpression<T, U>(this IOrderedQueryable<T> source, Expression<Func<T, U>> keySelector, bool descending = true)
    {
        if (descending)
        {
            return source.ThenByDescending(keySelector);
        }
        else
        {
            return source.ThenBy(keySelector);
        }
    }
}