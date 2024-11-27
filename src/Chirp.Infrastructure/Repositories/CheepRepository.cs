using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDbContext _context;
    
    public CheepRepository(ChirpDbContext context)
    {
        _context = context;
    }
    
    public CheepDTO ReadCheep(Cheep cheep)
    {
        return new CheepDTO
        {
            Text = cheep.Text,
            Timestamp = Convert(cheep.TimeStamp),
            Author = cheep.Author.UserName
        };
    }

    public void CreateCheep(Author author, String text, long timestamp)
    {
        // Converts info to cheep
        var cheep = new Cheep
        {
            Text = text,
            TimeStamp = DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime,
            AuthorId = author.Id,
            Author = author
        };
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();  // Saves the Cheep to the database
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