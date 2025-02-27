namespace UltimateUtils.Extensions;

public static class DecimalExtensions
{
    public static bool IsNegative(this decimal number)
    {
        return decimal.IsNegative(number);
    }

    public static bool IsPositive(this decimal number)
    {
        return decimal.IsPositive(number);
    }

    public static bool IsInteger(this decimal number)
    {
        return decimal.IsInteger(number);
    }

    public static bool IsEvenInteger(this decimal number)
    {
        return decimal.IsEvenInteger(number);
    }

    public static bool IsOddInteger(this decimal number)
    {
        return decimal.IsOddInteger(number);
    }
}
