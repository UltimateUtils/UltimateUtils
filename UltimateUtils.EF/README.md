# Ultimate Utils for EF Core

---

Includes all the features from [`UltimateUtils`](https://www.nuget.org/packages/UltimateUtils) since this package has a reference to it.
Please check the README of the package for details.

---

## Async Pagination with EF

With the reference to the package [EF Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore), the async pagination is supported.
You can use `.PaginateAsync()` as follows:

```csharp
queryable.PaginateAsync(pageNumber, pageSize, cancellationToken)
queryable.PaginateAsync(pageNumber, pageSize, converter, cancellationToken)

queryable.PaginateAsync(orderByKeySelectorExpression, descending, pageNumber, pageSize, cancellationToken)
queryable.PaginateAsync(orderByKeySelectorExpression, descending, pageNumber, pageSize, converter, cancellationToken)
```

`converter` is for projection that happens on the DB side as used in `queryable.Select()`. If your conversion is not doable on the DB side, you need to convert yourself after getting the `PagedList<T>` as `queryable.Paginate(...).Select(...)`

`orderByKeySelectorExpression` is an argument of type `Expression<Func<TSource, TKey>>` to select the `OrderBy` property. You can use lambda expression for that.
