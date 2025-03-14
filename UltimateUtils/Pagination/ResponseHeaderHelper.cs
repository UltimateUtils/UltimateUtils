using Microsoft.AspNetCore.Http;
using UltimateUtils.Abstraction.Pagination;

namespace UltimateUtils.Pagination;

public static class ResponseHeaderHelper
{
    public static IPagedList<T> SettingPaginationHeaders<T>(
        this IPagedList<T> pagedList,
        IHeaderDictionary responseHeaders,
        Action<HeaderOptions>? action = null)
    {
        var headerOptions = new HeaderOptions();
        action?.Invoke(headerOptions);

        responseHeaders.Append(headerOptions.XCurrentCount, pagedList.Count.ToString());
        responseHeaders.Append(headerOptions.XTotalCount, pagedList.TotalItemCount.ToString());
        responseHeaders.Append(headerOptions.XTotalPages, pagedList.TotalPageCount.ToString());
        responseHeaders.Append(headerOptions.XPageNumber, pagedList.PageNumber.ToString());
        responseHeaders.Append(headerOptions.XPageSize, pagedList.PageSize.ToString());
        responseHeaders.Append(headerOptions.XHasPreviousPage, pagedList.HasPreviousPage.ToString());
        responseHeaders.Append(headerOptions.XHasNextPage, pagedList.HasNextPage.ToString());

        return pagedList;
    }

    public static void SetPaginationHeaders<T>(
        this IHeaderDictionary responseHeaders,
        IPagedList<T> pagedList,
        Action<HeaderOptions>? action = null)
    {
        var headerOptions = new HeaderOptions();
        action?.Invoke(headerOptions);

        responseHeaders.Append(headerOptions.XCurrentCount, pagedList.Count.ToString());
        responseHeaders.Append(headerOptions.XTotalCount, pagedList.TotalItemCount.ToString());
        responseHeaders.Append(headerOptions.XTotalPages, pagedList.TotalPageCount.ToString());
        responseHeaders.Append(headerOptions.XPageNumber, pagedList.PageNumber.ToString());
        responseHeaders.Append(headerOptions.XPageSize, pagedList.PageSize.ToString());
        responseHeaders.Append(headerOptions.XHasPreviousPage, pagedList.HasPreviousPage.ToString());
        responseHeaders.Append(headerOptions.XHasNextPage, pagedList.HasNextPage.ToString());
    }
}
