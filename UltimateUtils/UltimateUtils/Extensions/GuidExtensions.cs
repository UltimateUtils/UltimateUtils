namespace UltimateUtils.Extensions;

public static class GuidExtensions
{
    public static Guid ToGuid(this string str)
    {
        return Guid.Parse(str);
    }
}
