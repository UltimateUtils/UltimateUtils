using System.Linq.Expressions;

namespace UltimateUtils.Pagination;

public class PagedOrderedList<T, TKey> : PagedListBase<T>
{
    internal PagedOrderedList(
        IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> keySelectorExpression,
        bool descending = false)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                descending
                    ? sourceItems
                        .OrderByDescending(keySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable()
                    : sourceItems
                        .OrderBy(keySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable();

            CurrentPageItems.AddRange(items);
        }
    }

    internal PagedOrderedList(
        IEnumerable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> keySelectorExpression,
        bool descending = false)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending)
    {
    }
}

public class PagedOrderedList<TSource, TKey, TResult> : PagedListBase<TResult>
{
    internal PagedOrderedList(
        IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> keySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                descending
                    ? sourceItems
                        .OrderByDescending(keySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable()
                    : sourceItems
                        .OrderBy(keySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable();

            CurrentPageItems.AddRange(items.Select(converter));
        }
    }

    internal PagedOrderedList(
        IEnumerable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> keySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending,
            converter)
    {
    }
}
