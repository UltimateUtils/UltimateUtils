namespace UltimateUtils.Extensions;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string input, bool ignoreCase = false)
        where TEnum : struct, Enum
    {
        if (Enum.TryParse(input, ignoreCase, out TEnum result))
        {
            return result;
        }

        throw new ArgumentException($"Cannot convert {input} to a value of the {typeof(TEnum).FullName} enumeration.");
    }

    public static TEnumTo ConvertEnum<TEnumFrom, TEnumTo>(this TEnumFrom input, bool ignoreCase = false)
        where TEnumFrom : struct, Enum
        where TEnumTo : struct, Enum
    {
        return input.ToString().ToEnum<TEnumTo>(ignoreCase);
    }

    public static TEnumTo? ConvertEnum<TEnumFrom, TEnumTo>(this TEnumFrom? input, bool ignoreCase = false)
        where TEnumFrom : struct, Enum
        where TEnumTo : struct, Enum
    {
        return input?.ToString().ToEnum<TEnumTo>(ignoreCase);
    }
}
