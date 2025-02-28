using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace UltimateUtils.Extensions;

public static partial class FloatExtensions
{
    #region Parse

    public static float ParseToFloat(this string number)
    {
        return float.Parse(number);
    }

    public static float ParseToFloat(this string number, NumberStyles style)
    {
        return float.Parse(number, style);
    }

    public static float ParseToFloat(this string number, IFormatProvider? provider)
    {
        return float.Parse(number, provider);
    }

    public static float ParseToFloat(
        this string number,
        NumberStyles style,
        IFormatProvider? provider)
    {
        return float.Parse(number, style, provider);
    }

    public static float ParseToFloat(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider)
    {
        return float.Parse(number, provider);
    }

    public static float ParseToFloat(
        this ReadOnlySpan<char> number,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null)
    {
        return float.Parse(number, style, provider);
    }

    public static float ParseToFloat(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider)
    {
        return float.Parse(number, provider);
    }

    public static float ParseToFloat(
        this ReadOnlySpan<byte> number,
        NumberStyles style = NumberStyles.Float | NumberStyles.AllowThousands,
        IFormatProvider? provider = null)
    {
        return float.Parse(number, style, provider);
    }

    #endregion Parse

    #region TryParse

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        out float result)
    {
        return float.TryParse(number, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(number, provider, out result);
    }

    public static bool TryParse(
        [NotNullWhen(true)] this string? number,
        NumberStyles style,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        out float result)
    {
        return float.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(
            number,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<char> number,
        NumberStyles style,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(
            number,
            style,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        out float result)
    {
        return float.TryParse(number, out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(
            number,
            provider,
            out result);
    }

    public static bool TryParse(
        this ReadOnlySpan<byte> number,
        NumberStyles style,
        IFormatProvider? provider,
        out float result)
    {
        return float.TryParse(
            number,
            style,
            provider,
            out result);
    }

    #endregion TryParse
}
