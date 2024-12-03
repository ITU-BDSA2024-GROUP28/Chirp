using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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
            Author = cheep.Author.UserName,
            CheepId = cheep.CheepId,
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

    public void DeleteCheep(int cheepId)
    {
        var cheep = _context.Cheeps.Find(cheepId);
        _context.Cheeps.Remove(cheep);
        _context.SaveChanges();
    }

    public async Task<IEnumerable<CheepDTO>> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize)
    {
        var query = _context.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName)) //If the list of authors, contains the author of the cheep, then we want the cheep
            .Select(cheep => new {cheep.Author.UserName, cheep.CheepId, cheep.TimeStamp, cheep.Text})
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var cheeps = await query
            .Select(cheep => new CheepDTO(){Text = cheep.Text, Timestamp = Time.ConvertToLong(cheep.TimeStamp), Author = cheep.UserName, CheepId = cheep.CheepId,}).ToListAsync();
        
        return cheeps;
    }
}