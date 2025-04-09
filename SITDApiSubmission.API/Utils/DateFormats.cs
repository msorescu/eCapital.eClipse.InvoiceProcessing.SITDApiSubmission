namespace SITDApiSubmission.API.Utils;
public static class DateFormats
{
    const string FULL_FORMAT = "yyyy-MM-ddTHH:mm:sszzz";

    public static string FullDateFormatString
    {
        get
        {
            return FULL_FORMAT;
        }
    }

    public static string FullDateString(DateTimeOffset dto)
    {
        return dto.ToString(FULL_FORMAT);
    }

    public static string FullDateString(DateTime dt)
    {
        return dt.ToUniversalTime().ToString(FULL_FORMAT);
    }

    public static string FullDateString(DateTime dt, TimeSpan offset)
    {
        DateTimeOffset dto = new(dt, offset);
        return dto.ToString(FULL_FORMAT);
    }
}
