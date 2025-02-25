namespace UltimateUtils.Extensions;

public static partial class DateTimeExtensions
{
    public static DateTime At(this DateTime date, TimeOnly time)
    {
        return new DateTime(date.ToDateOnly(), time);
    }

    public static DateTime At(this DateTime date, TimeOnly time, DateTimeKind kind)
    {
        return new DateTime(date.ToDateOnly(), time, kind);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second,
        DateTimeKind kind)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second,
            kind);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second,
        int millisecond)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second,
            millisecond);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second,
        int millisecond,
        DateTimeKind kind)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second,
            millisecond,
            kind);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second,
        int millisecond,
        int microsecond)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second,
            millisecond,
            microsecond);
    }

    public static DateTime At(
        this DateTime date,
        int hour,
        int minute,
        int second,
        int millisecond,
        int microsecond,
        DateTimeKind kind)
    {
        return new DateTime(
            date.Year,
            date.Month,
            date.Day,
            hour,
            minute,
            second,
            millisecond,
            microsecond,
            kind);
    }
}
