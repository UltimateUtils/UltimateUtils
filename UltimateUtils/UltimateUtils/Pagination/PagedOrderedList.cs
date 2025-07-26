using System.Linq.Expressions;
using UltimateUtils.Abstraction.Pagination;

namespace UltimateUtils.Pagination;

[Obsolete("Use UltimatePagination.PagedOrderList instead")]
public class PagedOrderedList<T, TKey> : PagedListBase<T>
{
    private PagedOrderedList(
        int pageNumber,
        int pageSize,
        int totalItemCount)
        : base(pageNumber, pageSize, totalItemCount)
    {
    }

    internal PagedOrderedList(
        IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending = false)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                descending
                    ? sourceItems
                        .OrderByDescending(orderByKeySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable()
                    : sourceItems
                        .OrderBy(orderByKeySelectorExpression)
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
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending = false)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending)
    {
    }

    public static IPagedList<T> Create(
        int pageNumber,
        int pageSize,
        int totalItemCount,
        IEnumerable<T> currentItems)
    {
        var pagedList = new PagedOrderedList<T, TKey>(pageNumber, pageSize, totalItemCount);

        if (totalItemCount > 0)
        {
            pagedList.CurrentPageItems.AddRange(currentItems);
        }

        return pagedList;
    }
}

public class PagedOrderedList<TSource, TKey, TResult> : PagedListBase<TResult>
{
    private PagedOrderedList(
        int pageNumber,
        int pageSize,
        int totalItemCount)
        : base(pageNumber, pageSize, totalItemCount)
    {
    }

    internal PagedOrderedList(
        IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                descending
                    ? sourceItems
                        .OrderByDescending(orderByKeySelectorExpression)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsEnumerable()
                    : sourceItems
                        .OrderBy(orderByKeySelectorExpression)
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
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending,
            converter)
    {
    }

    public static IPagedList<TResult> Create(
        int pageNumber,
        int pageSize,
        int totalItemCount,
        IEnumerable<TResult> currentItems)
    {
        var pagedList = new PagedOrderedList<TSource, TKey, TResult>(pageNumber, pageSize, totalItemCount);

        if (totalItemCount > 0)
        {
            pagedList.CurrentPageItems.AddRange(currentItems);
        }

        return pagedList;
    }
}
