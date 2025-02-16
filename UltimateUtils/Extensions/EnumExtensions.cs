namespace UltimateUtils.Extensions;

public static class EnumExtensions
{
    public static T ToEnum<T>(this string input, bool ignoreCase = false)
        where T : struct, Enum
    {
        if (Enum.TryParse(input, ignoreCase, out T result))
        {
            return result;
        }

        throw new ArgumentException($"Cannot convert {input} to a value of the {typeof(T).FullName} enumeration.");
    }

    public static TResult ConvertEnum<TInput, TResult>(this TInput input)
        where TInput : struct, Enum
        where TResult : struct, Enum
    {
        return input.ToString().ToEnum<TResult>();
    }

    public static TResult? ConvertEnum<TInput, TResult>(this TInput? input)
        where TInput : struct, Enum
        where TResult : struct, Enum
    {
        return input?.ToString().ToEnum<TResult>();
    }
}
