namespace UltimateUtils.Extensions;

public static partial class StringExtensions
{
    public static int ToInt(this string? number)
    {
        return Convert.ToInt32(number);
    }

    public static int ToInt(this string? number, IFormatProvider? provider)
    {
        return Convert.ToInt32(number, provider);
    }

    public static long ToLong(this string? number)
    {
        return Convert.ToInt64(number);
    }

    public static long ToLong(this string? number, IFormatProvider? provider)
    {
        return Convert.ToInt64(number, provider);
    }

    public static short ToShort(this string? number)
    {
        return Convert.ToInt16(number);
    }

    public static short ToShort(this string? number, IFormatProvider? provider)
    {
        return Convert.ToInt16(number, provider);
    }

    public static uint ToUInt(this string? number)
    {
        return Convert.ToUInt32(number);
    }

    public static uint ToUInt(this string? number, IFormatProvider? provider)
    {
        return Convert.ToUInt32(number, provider);
    }

    public static ulong ToULong(this string? number)
    {
        return Convert.ToUInt64(number);
    }

    public static ulong ToULong(this string? number, IFormatProvider? provider)
    {
        return Convert.ToUInt64(number, provider);
    }

    public static ushort ToUShort(this string? number)
    {
        return Convert.ToUInt16(number);
    }

    public static ushort ToUShort(this string? number, IFormatProvider? provider)
    {
        return Convert.ToUInt16(number, provider);
    }
}
