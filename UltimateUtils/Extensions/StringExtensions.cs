namespace UltimateUtils.Extensions;

public static partial class StringExtensions
{
    public static bool IsNullOrEmpty(this string? arg)
    {
        return string.IsNullOrEmpty(arg);
    }

    public static bool IsNullOrWhiteSpace(this string? arg)
    {
        return string.IsNullOrWhiteSpace(arg);
    }

    public static bool IsNotNullOrEmpty(this string? arg)
    {
        return !string.IsNullOrEmpty(arg);
    }

    public static bool IsNotNullOrWhiteSpace(this string? arg)
    {
        return !string.IsNullOrWhiteSpace(arg);
    }

    public static string JoinToString(this IEnumerable<string> items, string separator = "")
    {
        return string.Join(separator, items);
    }

    public static string JoinToString<T>(this IEnumerable<T> items, string separator = "")
    {
        return string.Join(separator, items);
    }
}
