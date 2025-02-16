namespace UltimateUtils.Pagination;

public static class PaginationHelper
{
    #region Paginate

    public static IPagedList<T> Paginate<T>(
        this IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize)
    {
        return new PagedList<T>(
            sourceItems,
            pageNumber,
            pageSize);
    }

    public static IPagedList<T> Paginate<T>(
        this IEnumerable<T> sourceItems,
        int pageNumber,
        int pageSize)
    {
        return new PagedList<T>(
            sourceItems,
            pageNumber,
            pageSize);
    }

    public static IPagedList<TResult> Paginate<TSource, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
    {
        return new PagedList<TSource, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            converter);
    }

    public static IPagedList<TResult> Paginate<TSource, TResult>(
        this IEnumerable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
    {
        return new PagedList<TSource, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            converter);
    }

    #endregion

    #region Wrap

    public static PagedListWrapper<T> Wrap<T>(this IPagedList<T> pagedList)
    {
        return new PagedListWrapper<T>
        {
            TotalPageCount = pagedList.TotalPageCount,
            TotalItemCount = pagedList.TotalItemCount,
            PageNumber = pagedList.PageNumber,
            PageSize = pagedList.PageSize,
            HasPreviousPage = pagedList.HasPreviousPage,
            HasNextPage = pagedList.HasNextPage,
            Items = pagedList.ToList(),
        };
    }

    #endregion
}
