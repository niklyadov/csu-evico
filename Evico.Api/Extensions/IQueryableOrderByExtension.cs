using System.Linq.Expressions;

namespace Evico.Api.Extensions;

public static class IQueryableOrderByExtension
{
    public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector, bool desc = false)
    {
        return desc ? source.OrderByDescending(keySelector) : source.OrderBy(keySelector);
    }
}