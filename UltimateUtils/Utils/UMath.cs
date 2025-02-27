namespace UltimateUtils.Utils;

public static class UMath
{
    public static int GetGcd(int a, int b)
    {
        return _GetGcd(a, b);
    }

    public static int Gcd(int a, int b)
    {
        return _GetGcd(a, b);
    }

    public static long GetGcd(long a, long b)
    {
        return _GetGcd(a, b);
    }

    public static long Gcd(long a, long b)
    {
        return _GetGcd(a, b);
    }

    public static int GetLcm(int a, int b)
    {
        return _GetLcm(a, b);
    }

    public static int Lcm(int a, int b)
    {
        return _GetLcm(a, b);
    }

    public static long GetLcm(long a, long b)
    {
        return _GetLcm(a, b);
    }

    public static long Lcm(long a, long b)
    {
        return _GetLcm(a, b);
    }

    public static long Pow(int n, int power)
    {
        return (long)Math.Pow(n, power);
    }

    public static Dictionary<int, int> GetPrimeFactorization(int number)
    {
        return _GetPrimeFactorization(number);
    }

    public static Dictionary<int, int> PrimeFactorization(int number)
    {
        return _GetPrimeFactorization(number);
    }

    public static Dictionary<long, int> GetPrimeFactorization(long number)
    {
        return _GetPrimeFactorization(number);
    }

    public static Dictionary<long, int> PrimeFactorization(long number)
    {
        return _GetPrimeFactorization(number);
    }

    #region private

    private static int _GetGcd(int a, int b)
    {
        while (b != 0)
        {
            int temp = a;
            a = b;
            b = temp % b;
        }

        return a;
    }

    private static long _GetGcd(long a, long b)
    {
        while (b != 0)
        {
            long temp = a;
            a = b;
            b = temp % b;
        }

        return a;
    }

    private static int _GetLcm(int a, int b)
    {
        return a * b / _GetGcd(a, b);
    }

    private static long _GetLcm(long a, long b)
    {
        return a * b / _GetGcd(a, b);
    }

    private static Dictionary<int, int> _GetPrimeFactorization(int number)
    {
        if (number <= 0)
            throw new ArgumentException("Should be positive.", nameof(number));

        var primeFactorization = new Dictionary<int, int>();

        int factor = 2;

        while (factor <= number)
        {
            int count = 0;

            while (number % factor == 0)
            {
                number /= factor;
                ++count;
            }

            if (count != 0)
                primeFactorization[factor] = count;

            ++factor;
        }

        return primeFactorization;
    }

    private static Dictionary<long, int> _GetPrimeFactorization(long number)
    {
        if (number <= 0)
            throw new ArgumentException("Should be positive.", nameof(number));

        var primeFactorization = new Dictionary<long, int>();

        int factor = 2;

        while (factor <= number)
        {
            int count = 0;

            while (number % factor == 0)
            {
                number /= factor;
                ++count;
            }

            if (count != 0)
                primeFactorization[factor] = count;

            ++factor;
        }

        return primeFactorization;
    }

    #endregion
}
