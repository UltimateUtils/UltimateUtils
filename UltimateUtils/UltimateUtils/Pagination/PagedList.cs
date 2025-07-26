using UltimateUtils.Abstraction.Pagination;

namespace UltimateUtils.Pagination;

[Obsolete("Use UltimatePagination.PagedList instead")]
public class PagedList<T> : PagedListBase<T>
{
    private PagedList(
        int pageNumber,
        int pageSize,
        int totalItemCount)
        : base(pageNumber, pageSize, totalItemCount)
    {
    }

    internal PagedList(
        IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                sourceItems
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsEnumerable();

            CurrentPageItems.AddRange(items);
        }
    }

    internal PagedList(
        IEnumerable<T> sourceItems,
        int pageNumber,
        int pageSize)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize)
    {
    }

    public static IPagedList<T> Create(
        int pageNumber,
        int pageSize,
        int totalItemCount,
        IEnumerable<T> currentItems)
    {
        var pagedList = new PagedList<T>(pageNumber, pageSize, totalItemCount);

        if (totalItemCount > 0)
        {
            pagedList.CurrentPageItems.AddRange(currentItems);
        }

        return pagedList;
    }
}

public class PagedList<TSource, TResult> : PagedListBase<TResult>
{
    private PagedList(
        int pageNumber,
        int pageSize,
        int totalItemCount)
        : base(pageNumber, pageSize, totalItemCount)
    {
    }

    internal PagedList(
        IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
        : base(pageNumber, pageSize, sourceItems.Count())
    {
        if (TotalItemCount > 0)
        {
            var items =
                sourceItems
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsEnumerable();

            CurrentPageItems.AddRange(items.Select(converter));
        }
    }

    internal PagedList(
        IEnumerable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
        : this(
            sourceItems.AsQueryable(),
            pageNumber,
            pageSize,
            converter)
    {
    }

    public static IPagedList<TResult> Create(
        int pageNumber,
        int pageSize,
        int totalItemCount,
        IEnumerable<TResult> currentItems)
    {
        var pagedList = new PagedList<TSource, TResult>(pageNumber, pageSize, totalItemCount);

        if (totalItemCount > 0)
        {
            pagedList.CurrentPageItems.AddRange(currentItems);
        }

        return pagedList;
    }
}
