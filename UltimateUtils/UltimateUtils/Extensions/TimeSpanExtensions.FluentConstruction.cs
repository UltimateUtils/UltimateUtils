namespace UltimateUtils.Extensions;

public static class TimeSpanExtensions
{
    public static TimeSpan Days(this int days, params TimeSpan[] addition)
    {
        return
            addition
                .Aggregate(
                    TimeSpan.FromDays(days),
                    (current, time) => current + time);
    }

    public static TimeSpan Hours(this int hours, params TimeSpan[] addition)
    {
        return
            addition
                .Aggregate(
                    TimeSpan.FromHours(hours),
                    (current, time) => current + time);
    }

    public static TimeSpan Minutes(this int minutes, params TimeSpan[] addition)
    {
        return
            addition
                .Aggregate(
                    TimeSpan.FromMinutes(minutes),
                    (current, time) => current + time);
    }

    public static TimeSpan Seconds(this int seconds, params TimeSpan[] addition)
    {
        return
            addition
                .Aggregate(
                    TimeSpan.FromSeconds(seconds),
                    (current, time) => current + time);
    }
}
