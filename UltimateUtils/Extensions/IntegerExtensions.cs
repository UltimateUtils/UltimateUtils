namespace UltimateUtils.Extensions;

public static class IntegerExtensions
{
    public static bool IsEven(this int number)
    {
        return (number & 1) == 0;
    }

    public static bool IsOdd(this int number)
    {
        return (number & 1) != 0;
    }

    public static bool IsPowerOfTwo(this int number)
    {
        return (number & (number - 1)) == 0
            && number > 0;
    }

    public static bool IsPositive(this int number)
    {
        return number > 0;
    }

    public static bool IsZero(this int number)
    {
        return number == 0;
    }

    public static bool IsNegative(this int number)
    {
        return number < 0;
    }

    public static bool IsPrime(this int number)
    {
        if (number <= 1)
            return false;

        if (number % 2 == 0 || number % 3 == 0)
            return number is 2 or 3;

        // Now, we've excluded all even numbers and 3's multiples
        // so we don't have to check their multiples

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }

        return true;
    }
}
