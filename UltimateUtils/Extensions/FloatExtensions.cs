namespace UltimateUtils.Extensions;

public static class FloatExtensions
{
    public static bool IsFinite(this float number)
    {
        return float.IsFinite(number);
    }

    public static bool IsInfinity(this float number)
    {
        return float.IsInfinity(number);
    }

    public static bool IsNaN(this float number)
    {
        return float.IsNaN(number);
    }

    public static bool IsNegative(this float number)
    {
        return float.IsNegative(number);
    }

    public static bool IsPositive(this float number)
    {
        return float.IsPositive(number);
    }

    public static bool IsNegativeInfinity(this float number)
    {
        return float.IsNegativeInfinity(number);
    }

    public static bool IsPositiveInfinity(this float number)
    {
        return float.IsPositiveInfinity(number);
    }

    public static float ParseToFloat(this string number)
    {
        return float.Parse(number);
    }

    public static bool IsPowerOfTwo(this float number)
    {
        return float.IsPow2(number);
    }

    public static bool IsInteger(this float number)
    {
        return float.IsInteger(number);
    }

    public static bool IsEvenInteger(this float number)
    {
        return float.IsEvenInteger(number);
    }

    public static bool IsOddInteger(this float number)
    {
        return float.IsOddInteger(number);
    }

    public static float Pow(this float number, float power)
    {
        return float.Pow(number, power);
    }

    public static float RootN(this float number, int n)
    {
        return float.RootN(number, n);
    }

    public static float Sqrt(this float number)
    {
        return float.Sqrt(number);
    }
}
