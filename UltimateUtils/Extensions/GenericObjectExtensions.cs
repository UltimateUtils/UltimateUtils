namespace UltimateUtils.Extensions;

public static class GenericObjectExtensions
{
    public static void EnsureNotNull<T>(this T? obj, string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(obj, paramName);
    }

    public static T EnsuringNotNull<T>(this T? obj, string? paramName = null)
        where T : class
    {
        return obj ?? throw new ArgumentNullException(paramName ?? nameof(obj));
    }
}
