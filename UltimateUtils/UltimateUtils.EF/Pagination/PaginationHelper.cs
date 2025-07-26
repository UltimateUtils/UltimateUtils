using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UltimateUtils.Abstraction.Pagination;
using UltimateUtils.Pagination;

namespace UltimateUtils.EF.Pagination;

[Obsolete("Use UltimatePagination.EF.PaginationHelper instead")]
public static class PaginationHelper
{
    #region PaginateAsync -> PagedList

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<T>> PaginateAsync<T>(
        this IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedList<T>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            await sourceItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

        return PagedList<T>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems);
    }

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<TResult>> PaginateAsync<TSource, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedList<TSource, TResult>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            await sourceItems
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

        return PagedList<TSource, TResult>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems.Select(converter));
    }

    #endregion

    #region PaginateAsync -> PagedOrderedList

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<T>> PaginateAsync<T, TKey>(
        this IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedOrderedList<T, TKey>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            descending
                ? await sourceItems
                    .OrderByDescending(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

        return PagedOrderedList<T, TKey>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems);
    }

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<TResult>> PaginateAsync<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        Func<TSource, TResult> converter,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedOrderedList<TSource, TKey, TResult>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            descending
                ? await sourceItems
                    .OrderByDescending(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

        return PagedOrderedList<TSource, TKey, TResult>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems.Select(converter));
    }

    #endregion

    #region PaginateAsync 2 -> PagedOrderedList

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<T>> PaginateAsync<T, TKey>(
        this IQueryable<T> sourceItems,
        Expression<Func<T, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedOrderedList<T, TKey>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            descending
                ? await sourceItems
                    .OrderByDescending(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

        return PagedOrderedList<T, TKey>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems);
    }

    [Obsolete("Use UltimatePagination.EF.PaginationHelper.PaginateAsync() instead")]
    public static async Task<IPagedList<TResult>> PaginateAsync<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        Expression<Func<TSource, TKey>> orderByKeySelectorExpression,
        bool descending,
        int pageNumber,
        int pageSize,
        Func<TSource, TResult> converter,
        CancellationToken cancellationToken = default)
    {
        int totalItemCount = await sourceItems.CountAsync(cancellationToken).ConfigureAwait(false);

        if (totalItemCount <= 0)
        {
            return PagedOrderedList<TSource, TKey, TResult>.Create(
                pageNumber,
                pageSize,
                totalItemCount,
                []);
        }

        var currentItems =
            descending
                ? await sourceItems
                    .OrderByDescending(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(orderByKeySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

        return PagedOrderedList<TSource, TKey, TResult>.Create(
            pageNumber,
            pageSize,
            totalItemCount,
            currentItems.Select(converter));
    }

    #endregion
}
