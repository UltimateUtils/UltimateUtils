using System.Linq.Expressions;

namespace UltimateUtils.Pagination;

public static class PaginationHelper
{
    #region Paginate -> PagedList

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

    #region Paginate -> PagedOrderedList

    public static IPagedList<T> Paginate<T, TKey>(
        this IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> keySelectorExpression,
        bool descending)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending);
    }

    public static IPagedList<T> Paginate<T, TKey>(
        this IEnumerable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> keySelectorExpression,
        bool descending)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> keySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending,
            converter);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IEnumerable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> keySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            keySelectorExpression,
            descending,
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
