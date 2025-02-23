namespace UltimateUtils.Extensions;

public static class DateAndTimeConverters
{
    #region Datetime

    public static DateOnly ToDateOnly(this DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }

    public static TimeOnly ToTimeOnly(this DateTime dateTime)
    {
        return TimeOnly.FromDateTime(dateTime);
    }

    #endregion

    #region TimeSpan

    public static TimeOnly ToTimeOnly(this TimeSpan timeSpan)
    {
        return TimeOnly.FromTimeSpan(timeSpan);
    }

    #endregion

    #region DateOnly

    public static DateTime ToDateTime(this DateOnly dateOnly)
    {
        return dateOnly.ToDateTime(TimeSpan.Zero.ToTimeOnly());
    }

    #endregion
}
