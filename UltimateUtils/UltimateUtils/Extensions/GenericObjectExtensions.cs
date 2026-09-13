using System.Diagnostics.CodeAnalysis;

namespace UltimateUtils.Extensions;

public static class GenericObjectExtensions
{
    public static void EnsureNotNull<T>([NotNull] this T? argument, string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
    }

    public static T EnsuringNotNull<T>([NotNull] this T? argument, string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        return argument;
    }

    public static void EnsureNotNull<T>([NotNull] this T? argument, string? paramName = null)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
    }

    public static T EnsuringNotNull<T>([NotNull] this T? argument, string? paramName = null)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);

        return argument.Value;
    }

    public static bool IsNull<T>([NotNullWhen(false)] this T? argument)
    {
        return argument is null;
    }

    public static bool IsNotNull<T>([NotNullWhen(true)] this T? argument)
    {
        return argument is not null;
    }
}
