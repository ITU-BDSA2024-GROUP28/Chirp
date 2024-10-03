using Chirp.Razor;
using Chirp.Razor.CheepRepository;
using Microsoft.EntityFrameworkCore;
using MyChat.Razor;

public class ChatService : IChatService
{
    // These would normally be loaded from a database for example
    
    ICheepRepository _cheepRepository;
    
    
    
    public List<Cheep> GetCheeps()
    {
        List<Cheep> cheeps = new List<Cheep>();
        return cheeps;
    }

    public List<Cheep> GetCheepsFromAuthor(string author)
    {
        // filter by the provided author name
        //return _cheeps.Where(x => x.Author == author).ToList();
        List<Cheep> cheeps = new List<Cheep>();
        return cheeps;
    }

    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

}