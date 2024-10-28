using System.Linq.Expressions;

namespace PlanSphere.Core.Extensions;

public static class QueryableExtensions
{
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