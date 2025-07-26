namespace UltimateUtils.Extensions;

public static class EnumExtensions
{
    public static TEnum ToEnum<TEnum>(this string input, bool ignoreCase = false)
        where TEnum : struct, Enum
    {
        return input._ToEnum<TEnum>(ignoreCase);
    }

    public static TEnumTo ConvertEnum<TEnumFrom, TEnumTo>(this TEnumFrom input, bool ignoreCase = false)
        where TEnumFrom : struct, Enum
        where TEnumTo : struct, Enum
    {
        return input.ToString()._ToEnum<TEnumTo>(ignoreCase);
    }

    public static TEnumTo? ConvertEnum<TEnumFrom, TEnumTo>(this TEnumFrom? input, bool ignoreCase = false)
        where TEnumFrom : struct, Enum
        where TEnumTo : struct, Enum
    {
        return input?.ToString()._ToEnum<TEnumTo>(ignoreCase);
    }

    #region private

    private static TEnum _ToEnum<TEnum>(this string input, bool ignoreCase) where TEnum : struct, Enum
    {
        if (Enum.TryParse(input, ignoreCase, out TEnum result))
        {
            return result;
        }

        throw new ArgumentException($"Cannot convert {input ?? "null"} to the {typeof(TEnum).FullName} enum value.");
    }

    #endregion
}
