namespace Chirp.Infrastructure.Services;

public class Time
{
    /*
     * Methods to convert the various types of time units
     * @param Datetime
     * @return a long
     */
    public static DateTime ConvertToDateTime(long timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime();
    }
    
    public static long ConvertToLong(DateTime dateTime)
    {
        return((DateTimeOffset)DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)).ToUnixTimeSeconds();
    }
    
    public static string ConvertToString(long timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
    }
}