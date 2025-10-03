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

    public static DateTimeOffset ToDateTimeOffset(this DateTime dateTime, TimeSpan? offset = null)
    {
        return
            offset is null
                ? new DateTimeOffset(dateTime)
                : new DateTimeOffset(dateTime, offset.Value);
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
