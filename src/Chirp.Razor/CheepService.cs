using Chirp.Razor;

public class CheepService : ICheepService
{
    public List<CheepDTO> GetCheeps()
    {
        return SQLiteDB.GetCheeps();
  }

    public List<CheepDTO> GetCheepsFromAuthor(string author)
    {

        return SQLiteDB.GetCheepsFromAuthor(author);
        // filter by the provided author name
    }
    }

   /* private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

}*/
