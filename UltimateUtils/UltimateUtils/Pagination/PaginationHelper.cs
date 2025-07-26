using System.Linq.Expressions;
using UltimateUtils.Abstraction.Pagination;

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
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending);
    }

    public static IPagedList<T> Paginate<T, TKey>(
        this IEnumerable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending,
            converter);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IEnumerable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending,
            converter);
    }

    #endregion

    #region Paginate 2 -> PagedOrderedList

    public static IPagedList<T> Paginate<T, TKey>(
        this IQueryable<T> sourceItems,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending);
    }

    public static IPagedList<T> Paginate<T, TKey>(
        this IEnumerable<T> sourceItems,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize)
    {
        return new PagedOrderedList<T, TKey>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
            descending,
            converter);
    }

    public static IPagedList<TResult> Paginate<TSource, TKey, TResult>(
        this IEnumerable<TSource> sourceItems,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter)
    {
        return new PagedOrderedList<TSource, TKey, TResult>(
            sourceItems,
            pageNumber,
            pageSize,
            orderByKeySelectorExpression,
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

    #region Convert

    public static IPagedList<TResult> Convert<TSource, TResult>(
        this IPagedList<TSource> pagedList,
        Func<TSource, TResult> converter)
    {
        return PagedList<TResult>.Create(
            pagedList.PageNumber,
            pagedList.PageSize,
            pagedList.TotalItemCount,
            pagedList.Select(converter));
    }

    #endregion Convert
}
