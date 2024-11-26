using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDbContext _context;
    
    public CheepRepository(ChirpDbContext context)
    {
        context = _context;
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

    public void CreateCheep(CheepDTO cheepDto)
    {
        // Retrieve the Author based on the author name
        var author = _context.Authors.FirstOrDefault(a => a.UserName == cheepDto.Author);
        if (author == null)
        {
            throw new ApplicationException("Author not found");
        }

        // Converts CheepDTO to Cheep entity
        var cheep = new Cheep
        {
            Text = cheepDto.Text,
            TimeStamp = DateTimeOffset.FromUnixTimeSeconds(cheepDto.Timestamp).DateTime,
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