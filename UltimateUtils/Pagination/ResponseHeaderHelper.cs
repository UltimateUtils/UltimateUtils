using Microsoft.AspNetCore.Http;
using UltimateUtils.Abstraction.Pagination;

namespace UltimateUtils.Pagination;

public static class ResponseHeaderHelper
{
    public static IPagedList<T> SettingPaginationHeaders<T>(
        this IPagedList<T> pagedList,
        IHeaderDictionary responseHeaders)
    {
        responseHeaders.Append(Constants.XCurrentCount, pagedList.Count.ToString());
        responseHeaders.Append(Constants.XTotalCount, pagedList.TotalItemCount.ToString());
        responseHeaders.Append(Constants.XTotalPages, pagedList.TotalPageCount.ToString());
        responseHeaders.Append(Constants.XPageNumber, pagedList.PageNumber.ToString());
        responseHeaders.Append(Constants.XPageSize, pagedList.PageSize.ToString());
        responseHeaders.Append(Constants.XHasPreviousPage, pagedList.HasPreviousPage.ToString());
        responseHeaders.Append(Constants.XHasNextPage, pagedList.HasNextPage.ToString());

        return pagedList;
    }

    public static void SetPaginationHeaders<T>(
        this IHeaderDictionary responseHeaders,
        IPagedList<T> pagedList)
    {
        responseHeaders.Append(Constants.XCurrentCount, pagedList.Count.ToString());
        responseHeaders.Append(Constants.XTotalCount, pagedList.TotalItemCount.ToString());
        responseHeaders.Append(Constants.XTotalPages, pagedList.TotalPageCount.ToString());
        responseHeaders.Append(Constants.XPageNumber, pagedList.PageNumber.ToString());
        responseHeaders.Append(Constants.XPageSize, pagedList.PageSize.ToString());
        responseHeaders.Append(Constants.XHasPreviousPage, pagedList.HasPreviousPage.ToString());
        responseHeaders.Append(Constants.XHasNextPage, pagedList.HasNextPage.ToString());
    }
}
