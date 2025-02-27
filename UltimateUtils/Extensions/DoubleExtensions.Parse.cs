using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace UltimateUtils.Extensions;

public static partial class DoubleExtensions
{
    #region Parse

    public static double ParseToDouble(this string number)
    {
        return double.Parse(number);
    }

    public static double ParseToDouble(this string number, NumberStyles style)
    {
        return double.Parse(number, style);
    }

    public static double ParseToDouble(this string number, IFormatProvider? provider)
    {
        return double.Parse(number, provider);
    }

    public static double ParseToDouble(
        this string number,
        NumberStyles style,
        IFormatProvider? provider)
    {
        return double.Parse(number, style, provider);
    }

    public static double ParseToDouble(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider)
    {
        return double.Parse(number, provider);
    }

    public static double ParseToDouble(
        this ReadOnlySpan<char> number,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null)
    {
        return double.Parse(number, style, provider);
    }

    public static double ParseToDouble(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider)
    {
        return double.Parse(number, provider);
    }

    public static double ParseToDouble(
        this ReadOnlySpan<byte> number,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null)
    {
        return double.Parse(number, style, provider);
    }

    #endregion Parse

    #region TryParse

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        out double result)
    {
        return double.TryParse(number, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        NumberStyles style,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        out double result)
    {
        return double.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        NumberStyles style,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        out double result)
    {
        return double.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        NumberStyles style,
        IFormatProvider? provider,
        out double result)
    {
        return double.TryParse(number, style, provider, out result);
    }

    #endregion TryParse
}
