# Ultimate Utils

---

## 1. Pagination

### 1.1. Paginate Helper Methods

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

#### 1.1.1 `.Paginate()` methods with `IQueryable<>`

`Paginate` method overloads:

```csharp
queryable.Paginate(pageNumber, pageSize)
queryable.Paginate(pageNumber, pageSize, converter)
queryable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize)
queryable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize, converter)
```

`converter` is for projection that happens on the DB side as used in `queryable.Select()`. If your conversion is not doable on the DB side, you need to convert yourself after getting the `PagedList<T>` as `queryable.Paginate(...).Select(...)`

`orderByKeySelectorExpression` is an argument of type `Expression<Func<TSource, TKey>>` to select the `OrderBy` property. You can use lambda expression for that.

#### 1.1.2. `.Paginate()` methods with `IEnumerable<>`

`Paginate` method overloads:

```csharp
enumerable.Paginate(pageNumber, pageSize)
enumerable.Paginate(pageNumber, pageSize, converter)
enumerable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize)
enumerable.Paginate(orderByKeySelectorExpression, descending, pageNumber, PageSize, converter)
```

They work almost the same as `IQueryable<>` versions except how `converter` works. The projection works in memory.

#### 1.1.3. `.Convert<TSource, TResult>(converter)`

```csharp
pagedList.Convert<StudentEntity, StudentDto>(s => s.ToDto());
```

`.Convert()` method converts `IPagedList<TSource>` to `IPagedList<Result>` by applying the converter to each item.

### 1.2. Response Header Helper for pagination info

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

---

## 2. Extensions

### 2.1. String Extensions

🔴 DON'T
```csharp
string.IsNullOrEmpty(arg);
string.IsNullOrWhiteSpace(arg);
!string.IsNullOrEmpty(arg);
!string.IsNullOrWhiteSpace(arg);
string.Join(separator, items);
```

🟢 DO
```csharp
arg.IsNullOrEmpty();
arg.IsNullOrWhiteSpace();
!arg.IsNullOrEmpty();
!arg.IsNullOrWhiteSpace();
items.JoinToString(separator);
```

Some extensions to/from String
```csharp
s.ParseToInt()
c.ParseToInt()
n.ToBinaryString()
n.ToOctalString()
n.ToHexString()
```
where s is string, c is char, n is integer.

### 2.2 Enumerable Extensions

Where `items` is `IEnumerable<>`
```csharp
items.IsEmpty(); // this means the same as the line below
!items.Any();
```
You can use `.IsEmpty()` to check if the `items` is empty or not instead of `! Any()`.

### 2.3. Integer Extensions

```csharp
.IsEven()
.IsOdd()
.IsPositive()
.IsZero()
.IsNegative()
```

### 2.4. DateTime Extensions

Fluent Construction for DateTime:

```csharp
var d1 = 2025.January(24);
var d2 = 2025.Jan(24);
var d3 = new DateTime(2025, 1, 24);
```
`d1`, `d2` and `d3` are the same.

You can append `Time` with `.At()` extension method overloads as follows.

```csharp
var d3 = 2025.Jan(24).At(14, 47, 39);
```
It's 2:47:39 PM on Jan 24th in 2025.

You can optionally specify `millisecond`, `microsecond`, `DateTimeKind` with `.At()` overloads.

You can specify `Calendar` with `.In()` method as follows:

```csharp
var d4 = 2025.Jan(24).At(14, 47, 39).In(new KoreanLunisolarCalendar());
```

### 2.5. TimeSpan Extensions

Fluent Construction for TimeSpan:

```csharp
var t1 = 2.Hours(13.Minutes(42.Seconds()));
var t2 = 2.Hours(13.Minutes, 42.Seconds());
var t3 = new TimeSpan(2, 13, 42);
```

`t1`, `t2`, `t3` are the same.

### 2.6. Float/Double/Decimal Extensions

Where `num` is of type float/double/decimal, you can do the following:

```csharp
num.IsFinite()
num.IsInfinity()
num.IsNaN()
num.IsNegative()
num.IsPositive()
num.IsNegativeInfinity()
num.IsPositiveInfinity()
num.IsInteger()
num.IsEvenInteger()
num.IsOddInteger()
num.Sqrt()
num.Abs()
num.Ceiling()
num.Floor()
```

among others.

You can do `Parse()` and `TryParse()` in fluent manner as follows:

```csharp
s.ParseToFloat()
s.ParseToDouble()
s.ParseToDecimal()
s.TryParse(out float result)
s.TryParse(out double result)
s.TryParse(out decimal result)
```
where `s` is string. You can optionally specify `NumberStyle` and `IFormatProvider` in some overloads.

### 2.6. Json Serialization Extensions

```csharp
value.Serialize(options); // Do this
JsonSerializer.Serialize(value, options); // instead of this
```

The methods above are identical.
There is a convenience method for "camelCase" key as follows.

```csharp
value.SerializeWithCamelCaseKey(indented: true);
value.SerializeWithCamelCaseKey(indented: false);
```

### 2.7. Guid Extensions

```csharp
str.ToGuid();
```

`ToGuid()` method on string value returns Guid object from the string representation. It throws exception if not successful.

---

## 3. Convertors

### 3.1. string -> numbers

```csharp
s.ToInt()
s.ToInt(IFormatProvider?)
f.ToInt()
d.ToInt()
dc.ToInt()

s.ToShort()
s.ToShort(IFormatProvider?)
f.ToShort()
d.ToShort()
dc.ToShort()

s.ToLong()
s.ToLong(IFormatProvider?)
f.ToLong()
d.ToLong()
c.ToLong()

s.ToUInt()
s.ToUInt(IFormatProvider?)
f.ToUInt()
d.ToUInt()
dc.ToUInt()

s.ToUShort()
s.ToUShort(IFormatProvider?)
f.ToUShort()
d.ToUShort()
dc.ToUShort()

s.ToULong()
s.ToULong(IFormatProvider?)
f.ToULong()
d.ToULong()
dc.ToULong()
```
where `s` is string, `f` is float, `d` is double, `dc` is decimal.
`U` means `unsigned` as in `ToUInt` meaning `to unsigned int`.

---

## 3. Null Helpers

```csharp
arg.EnsureNotNull()     -> throws if arg is null
arg.EnsuringNotNull()   -> throws if arg is null and return arg otherwise
```
They accept `paramName` optionally to log the parameter name.

---

## 4. Utils

### 5.1. Math

There are some static helper methods for mathematics.

```csharp
UMath.GetGcd(a, b)
UMath.Gcd(a, b)
UMath.GetLcm(a, b)
UMath.Lcm(a, b)
UMath.GetPrimeFactorization(number)
UMath.PrimeFactorization(number)
```
You can pass `int` or `long` for `a`, `b` and `number`
