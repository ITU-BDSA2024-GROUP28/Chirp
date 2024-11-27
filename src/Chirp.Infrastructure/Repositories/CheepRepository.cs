using Chirp.Core;
using Chirp.Infrastructure.Services;

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
            Timestamp = Time.ConvertToLong(cheep.TimeStamp),
            Author = cheep.Author.UserName
        };
    }

    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */
    public void CreateCheep(AuthorDTO author, CheepDTO cheepdto)
    {
        // Converts info to cheep
        var cheep = new Cheep
        {
            Text = cheepdto.Text,
            TimeStamp = Time.ConvertToDateTime(cheepdto.Timestamp),
            AuthorId = author.Id
        };
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();  // Saves the Cheep to the database
    }
}