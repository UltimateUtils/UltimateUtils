namespace UltimateUtils.Pagination;

public class PagedList<T> : PagedListBase<T>
{
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
}

public class PagedList<TSource, TResult> : PagedListBase<TResult>
{
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
}
