namespace UltimateUtils.Abstraction.Pagination;

[Obsolete("Use UltimatePagination.Abstraction.IPagedList instead")]
public interface IPagedList<out T> : IReadOnlyList<T>
{
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPageCount { get; }

    /// <summary>
    /// Total number of items in the page
    /// </summary>
    public int TotalItemCount { get; }

    /// <summary>
    /// 1-based index of the page
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Total number of items in the page
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Returns true if the page is not the first page
    /// </summary>
    public bool HasPreviousPage { get; }

    /// <summary>
    /// Returns true if the page is not the last page
    /// </summary>
    public bool HasNextPage { get; }
}
