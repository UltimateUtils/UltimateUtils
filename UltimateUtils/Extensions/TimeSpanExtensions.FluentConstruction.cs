namespace UltimateUtils.Extensions;

public static class TimeSpanExtensions
{
    public static TimeSpan Days(this int days, TimeSpan addition = default)
    {
        return TimeSpan.FromDays(days) + addition;
    }

    public static TimeSpan Hours(this int hours, TimeSpan addition = default)
    {
        return TimeSpan.FromHours(hours) + addition;
    }

    public static TimeSpan Minutes(this int minutes, TimeSpan addition = default)
    {
        return TimeSpan.FromMinutes(minutes) + addition;
    }

    public static TimeSpan Seconds(this int seconds, TimeSpan addition = default)
    {
        return TimeSpan.FromSeconds(seconds) + addition;
    }
}
