using System.Collections;
using UltimateUtils.Abstraction.Pagination;

namespace UltimateUtils.Pagination;

public abstract class PagedListBase<T> : IPagedList<T>
{
    protected readonly List<T> CurrentPageItems = [];

    protected internal PagedListBase(
        int pageNumber,
        int pageSize,
        int totalItemCount)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber, "PageNumber cannot be less than 1.");
        }

        if (pageSize < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize, "PageSize cannot be less than 1.");
        }

        if (totalItemCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(totalItemCount), totalItemCount, "TotalItemCount cannot be less than 0.");
        }

        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        PageNumber = pageNumber;

        TotalPageCount =
            TotalItemCount > 0
                ? (int)Math.Ceiling(TotalItemCount / (double)PageSize)
                : 0;

        bool isPageNumberGood = TotalPageCount > 0 && PageNumber <= TotalPageCount;
        HasPreviousPage = isPageNumberGood && PageNumber > 1;
        HasNextPage = isPageNumberGood && PageNumber < TotalPageCount;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return CurrentPageItems.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => CurrentPageItems.Count;

    public T this[int index] => CurrentPageItems[index];

    public int TotalPageCount { get; }

    public int TotalItemCount { get; }

    public int PageNumber { get; }

    public int PageSize { get; }

    public bool HasPreviousPage { get; }

    public bool HasNextPage { get; }
}
