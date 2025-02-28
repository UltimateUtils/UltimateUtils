namespace UltimateUtils.Extensions;

public static partial class DoubleExtensions
{
    public static int ToInt(this double number)
    {
        return Convert.ToInt32(number);
    }

    public static long ToLong(this double number)
    {
        return Convert.ToInt64(number);
    }

    public static short ToShort(this double number)
    {
        return Convert.ToInt16(number);
    }

    public static uint ToUInt(this double number)
    {
        return Convert.ToUInt32(number);
    }

    public static ulong ToULong(this double number)
    {
        return Convert.ToUInt64(number);
    }

    public static ushort ToUShort(this double number)
    {
        return Convert.ToUInt16(number);
    }
}
