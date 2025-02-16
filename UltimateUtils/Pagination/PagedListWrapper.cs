namespace UltimateUtils.Pagination;

public class PagedListWrapper<T>
{
    public int TotalPageCount { get; init; }

    public int TotalItemCount { get; init; }

    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public bool HasPreviousPage { get; init; }

    public bool HasNextPage { get; init; }

    public required IReadOnlyList<T> Items { get; init; }
}
