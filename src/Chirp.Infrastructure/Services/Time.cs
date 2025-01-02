namespace Chirp.Infrastructure.Services;

/// <summary>
/// This class provides the methods we use repeatedly to convert the various types of time units
/// Storing the methods here ensures consistency
/// </summary>

public static class Time
{
    public static DateTime ConvertToDateTime(long timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime();
    }
    
    public static long ConvertToLong(DateTime dateTime)
    {
        return((DateTimeOffset)DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)).ToUnixTimeSeconds();
    }
    
    public static string ConvertToString(long timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
    }
}