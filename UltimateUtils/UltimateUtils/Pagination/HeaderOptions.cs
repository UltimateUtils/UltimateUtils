namespace UltimateUtils.Pagination;

public record HeaderOptions
{
    public string XCurrentCount { get; set; } = Constants.XCurrentCount;

    public string XTotalCount { get; set; } = Constants.XTotalCount;

    public string XTotalPages { get; set; } = Constants.XTotalPages;

    public string XPageNumber { get; set; } = Constants.XPageNumber;

    public string XPageSize { get; set; } = Constants.XPageSize;

    public string XHasPreviousPage { get; set; } = Constants.XHasPreviousPage;

    public string XHasNextPage { get; set; } = Constants.XHasNextPage;
}
