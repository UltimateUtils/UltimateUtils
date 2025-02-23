namespace UltimateUtils.Extensions;

public static partial class DateTimeExtensions
{
    public static DateTime At(this DateTime dateTime, TimeOnly timeOnly)
    {
        return new DateTime(dateTime.ToDateOnly(), timeOnly);
    }

    public static DateTime At(this DateTime dateTime, int hour, int minute, int second)
    {
        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            hour,
            minute,
            second);
    }

    public static DateTime At(
        this DateTime dateTime,
        int hour,
        int minute,
        int second,
        int millisecond)
    {
        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            hour,
            minute,
            second,
            millisecond);
    }

    public static DateTime At(
        this DateTime dateTime,
        int hour,
        int minute,
        int second,
        int millisecond,
        int microsecond)
    {
        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            hour,
            minute,
            second,
            millisecond,
            microsecond);
    }
}
