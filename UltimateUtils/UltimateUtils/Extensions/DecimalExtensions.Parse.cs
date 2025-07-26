using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace UltimateUtils.Extensions;

public static partial class DecimalExtensions
{
    #region Parse

    public static decimal ParseToDecimal(this string number)
    {
        return decimal.Parse(number);
    }

    public static decimal ParseToDecimal(this string number, NumberStyles style)
    {
        return decimal.Parse(number, style);
    }

    public static decimal ParseToDecimal(this string number, IFormatProvider? provider)
    {
        return decimal.Parse(number, provider);
    }

    public static decimal ParseToDecimal(
        this string number,
        NumberStyles style,
        IFormatProvider? provider)
    {
        return decimal.Parse(number, style, provider);
    }

    public static decimal ParseToDecimal(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider)
    {
        return decimal.Parse(number, provider);
    }

    public static decimal ParseToDecimal(
        this ReadOnlySpan<char> number,
        NumberStyles style = NumberStyles.Number,
        IFormatProvider? provider = null)
    {
        return decimal.Parse(number, style, provider);
    }

    public static decimal ParseToDecimal(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider)
    {
        return decimal.Parse(number, provider);
    }

    public static decimal ParseToDecimal(
        this ReadOnlySpan<byte> number,
        NumberStyles style = NumberStyles.Number,
        IFormatProvider? provider = null)
    {
        return decimal.Parse(number, style, provider);
    }

    #endregion Parse

    #region TryParse

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        out decimal result)
    {
        return decimal.TryParse(number, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        NumberStyles style,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        out decimal result)
    {
        return decimal.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        NumberStyles style,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        out decimal result)
    {
        return decimal.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        NumberStyles style,
        IFormatProvider? provider,
        out decimal result)
    {
        return decimal.TryParse(
            number,
            style,
            provider,
            out result);
    }

    #endregion TryParse
}
