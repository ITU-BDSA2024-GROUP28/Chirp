public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    // These would normally be loaded from a database for example
    public static readonly List<CheepViewModel> _cheeps = new()
    {
        new CheepViewModel("Emma", "Hello, Helge and Adrian!", UnixTimeStampToDateTimeString(1727776680)),
        new CheepViewModel("Amira", "Welcome to our Chirp! web application built with razor pages",
            UnixTimeStampToDateTimeString(1727776690)),
        new CheepViewModel("Stine-Helena", "This is my branch on tests", UnixTimeStampToDateTimeString(1727776700))
    };

    public List<CheepViewModel> GetCheeps()
    {
        return _cheeps;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        return _cheeps.Where(x => x.Author == author).ToList();
    }

    public static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
}