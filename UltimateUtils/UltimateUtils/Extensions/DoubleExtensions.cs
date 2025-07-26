namespace UltimateUtils.Extensions;

public static partial class DoubleExtensions
{
    public static bool IsFinite(this double number)
    {
        return double.IsFinite(number);
    }

    public static bool IsInfinity(this double number)
    {
        return double.IsInfinity(number);
    }

    public static bool IsNaN(this double number)
    {
        return double.IsNaN(number);
    }

    public static bool IsNegative(this double number)
    {
        return double.IsNegative(number);
    }

    public static bool IsPositive(this double number)
    {
        return double.IsPositive(number);
    }

    public static bool IsNegativeInfinity(this double number)
    {
        return double.IsNegativeInfinity(number);
    }

    public static bool IsPositiveInfinity(this double number)
    {
        return double.IsPositiveInfinity(number);
    }

    public static bool IsPowerOfTwo(this double number)
    {
        return double.IsPow2(number);
    }

    public static bool IsInteger(this double number)
    {
        return double.IsInteger(number);
    }

    public static bool IsEvenInteger(this double number)
    {
        return double.IsEvenInteger(number);
    }

    public static bool IsOddInteger(this double number)
    {
        return double.IsOddInteger(number);
    }

    public static double Pow(this double number, double power)
    {
        return Math.Pow(number, power);
    }

    public static double RootN(this double number, int n)
    {
        return double.RootN(number, n);
    }

    public static double Sqrt(this double number)
    {
        return Math.Sqrt(number);
    }

    public static double Abs(this double number)
    {
        return Math.Abs(number);
    }

    public static double Ceiling(this double number)
    {
        return Math.Ceiling(number);
    }

    public static double Floor(this double number)
    {
        return Math.Floor(number);
    }
}
