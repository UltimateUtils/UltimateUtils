namespace UltimateUtils.Extensions;

public static partial class DecimalExtensions
{
    public static int ToInt(this decimal number)
    {
        return Convert.ToInt32(number);
    }

    public static long ToLong(this decimal number)
    {
        return Convert.ToInt64(number);
    }

    public static short ToShort(this decimal number)
    {
        return Convert.ToInt16(number);
    }

    public static uint ToUInt(this decimal number)
    {
        return Convert.ToUInt32(number);
    }

    public static ulong ToULong(this decimal number)
    {
        return Convert.ToUInt64(number);
    }

    public static ushort ToUShort(this decimal number)
    {
        return Convert.ToUInt16(number);
    }
}
