using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UltimateUtils.Pagination;

namespace UltimateUtils.EF.Pagination;

public static class PaginationHelper
{
    #region PaginateAsync -> PagedList

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

    public static async Task<IPagedList<T>> PaginateAsync<T, TKey>(
        this IQueryable<T> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<T, TKey>> keySelectorExpression,
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
                    .OrderByDescending(keySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(keySelectorExpression)
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

    public static async Task<IPagedList<TResult>> PaginateAsync<TSource, TKey, TResult>(
        this IQueryable<TSource> sourceItems,
        int pageNumber,
        int pageSize,
        Expression<Func<TSource, TKey>> keySelectorExpression,
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
                    .OrderByDescending(keySelectorExpression)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false)
                : await sourceItems
                    .OrderBy(keySelectorExpression)
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
