using System.Globalization;

namespace UltimateUtils.Extensions;

public static partial class DateTimeExtensions
{
    public static DateTime In(this DateTime dateTime, Calendar calendar)
    {
        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            dateTime.Hour,
            dateTime.Minute,
            dateTime.Second,
            dateTime.Millisecond,
            dateTime.Microsecond,
            calendar,
            dateTime.Kind);
    }
}
