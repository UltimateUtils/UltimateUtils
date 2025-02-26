namespace UltimateUtils.Extensions;

public static class GenericObjectExtensions
{
    public static void EnsureNotNull<T>(this T? argument, string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
    }

    public static T EnsuringNotNull<T>(this T? argument, string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        return argument;
    }
}
