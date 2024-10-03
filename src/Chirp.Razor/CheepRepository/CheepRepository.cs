using Microsoft.EntityFrameworkCore;
using MyChat.Razor;

namespace Chirp.Razor.CheepRepository;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _dbContext;
    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> CreateCheep(Cheep cheep)
    {
        Cheep newCheep = new() { Text = cheep.Text, Author = cheep.Author };
        var queryResult = await _dbContext.cheeps.AddAsync(newCheep); // does not write to the database!

        await _dbContext.SaveChangesAsync(); // persist the changes in the database
        return queryResult.Entity.CheepId;
    }
    public async Task<List<Cheep>> ReadCheeps()
    {
        // Formulate the query - will be translated to SQL by EF Core
        var query = _dbContext.cheeps.Select(cheep => new { cheep.Author, cheep.Text });
        // Execute the query
        var result = await query.ToListAsync();

        List<Cheep> cheeps = new List<Cheep>();

        foreach (var cheep in query)
        {
            cheeps.Add( new Cheep() {Author = cheep.Author, Text = cheep.Text });
        }

        return cheeps;
    }
    public async Task<List<Cheep>> ReadCheeps(string userName)
    {
        // Formulate the query - will be translated to SQL by EF Core
        var query = _dbContext.cheeps.Select(cheep => new { cheep.Author, cheep.Text }).Where(cheep => cheep.Author.Equals(userName));
        // Execute the query
        var result = await query.ToListAsync();

        List<Cheep> cheeps = new List<Cheep>();

        foreach (var cheep in query)
        {
            cheeps.Add( new Cheep() {Author = cheep.Author, Text = cheep.Text });
        }

        return cheeps;
    }
}