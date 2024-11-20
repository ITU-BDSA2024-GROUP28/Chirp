using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    
    public CheepDTO ReadCheep(Cheep cheep)
    {
        return new CheepDTO(cheep.Text, Convert(cheep.TimeStamp), cheep.Author.UserName);
    }
    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */

    public static long Convert(DateTime dateTime)
    {
        return((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
    /*
     * Method to convert the time and date to UnixTime
     * @param Datetime
     * @return a long
     */
}