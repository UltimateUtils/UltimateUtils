namespace UltimateUtils.Extensions;

public static partial class FloatExtensions
{
    public static int ToInt(this float number)
    {
        return Convert.ToInt32(number);
    }

    public static long ToLong(this float number)
    {
        return Convert.ToInt64(number);
    }

    public static short ToShort(this float number)
    {
        return Convert.ToInt16(number);
    }

    public static uint ToUInt(this float number)
    {
        return Convert.ToUInt32(number);
    }

    public static ulong ToULong(this float number)
    {
        return Convert.ToUInt64(number);
    }

    public static ushort ToUShort(this float number)
    {
        return Convert.ToUInt16(number);
    }
}
