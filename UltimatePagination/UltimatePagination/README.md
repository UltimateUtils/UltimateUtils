# Ultimate Pagination

---

## 1. Paginate Helper Methods

Use `.Paginate()` helper methods to get `IPagedList<>` object, which you can return from your endpoint to the caller expecting `IEnumerable<>`.
In addition, it has the following properties so you can set response headers from them.

```text
TotalPageCount: int     -> how many pages you have in total
TotalItemCount: int     -> how many items you have in total
PageNumber: int         -> current page number
PageSize: int           -> current page size
HasPreviousPage: bool   -> whether it's the first page or not
HasNextPage: bool       -> whether it's the last page or not
```

### 1.1 `.Paginate()` methods with `IQueryable<>`

`Paginate` method overloads:

```csharp
queryable.Paginate(pageNumber, pageSize)
queryable.Paginate(pageNumber, pageSize, converter)

queryable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize)
queryable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize, converter)

queryable.Paginate(pageNumber, PageSize, orderByKeySelectorExpression, descending)
queryable.Paginate(pageNumber, PageSize, orderByKeySelectorExpression, descending, converter)
```

`converter` is for projection that happens on the DB side as used in `queryable.Select()`. If your conversion is not doable on the DB side, you need to convert yourself after getting the `PagedList<T>` as `queryable.Paginate(...).Select(...)`

`orderByKeySelectorExpression` is an argument of type `Expression<Func<TSource, TKey>>` to select the `OrderBy` property. You can use lambda expression for that.

### 1.2. `.Paginate()` methods with `IEnumerable<>`

`Paginate` method overloads:

```csharp
enumerable.Paginate(pageNumber, pageSize)
enumerable.Paginate(pageNumber, pageSize, converter)

enumerable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize)
enumerable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize, converter)

enumerable.Paginate(pageNumber, PageSize, orderByKeySelectorExpression, descending)
enumerable.Paginate(pageNumber, PageSize, orderByKeySelectorExpression, descending, converter)
```

They work almost the same as `IQueryable<>` versions except how `converter` works. The projection works in memory.

### 1.3. `.Convert<TSource, TResult>(converter)`

```csharp
pagedList.Convert<StudentEntity, StudentDto>(s => s.ToDto());
```

`.Convert()` method converts `IPagedList<TSource>` to `IPagedList<Result>` by applying the converter to each item.


## 2. Response Header Helper for pagination info

You can use `SetPaginationHeaders<T>()` and `SettingPaginationHeaders<T>()` methods to set pagination-related HTTP response headers.

- `Response.Headers` extension method. `SetPaginationHeaders<T>()` is a void method. You need to return pagedList to the endpoint caller.
```csharp
// pagedList -> IPagedList<T> type
Response.Headers.SetPaginationHeaders(pagedList);
```

- `IPagedList<T>` extension method. `SettingPaginationHeaders<T>()` returns pagedList so you can return it to the endpoint caller.
```csharp
var ret = pagedList.SettingPaginationHeaders(Response.Headers);
// ret -> IPagedList<T> type
return ret;
```

The methods above set the following headers:
```csharp
x-current-count
x-total-count
x-total-pages
x-page-number
x-page-size
x-has-previous-page
x-has-next-page
```

You can optionally pass `Action<HeaderOptions> action` to the methods to modify the headers keys above:
```csharp
Response.Headers
    .SetPaginationHeaders(
        pagedList,
        options =>
        {
            options.XCurrentCount = "new-key-for-x-current-count";
            options.XTotalCount = "new-key-for-x-total-count";
            options.XTotalPages = "new-key-for-x-total-pages";
            options.XPageNumber = "new-key-for-x-page-number";
            options.XPageSize = "new-key-for-x-page-size";
            options.XHasPreviousPage = "new-key-for-x-has-previous-page";
            options.XHasNextPage = "new-key-for-x-has-next-page";
        });
```

You don't have to set all of them. If you miss some, the default keys will be used.
