using MyChat.Razor;

namespace Chirp.Razor.CheepRepository;

public class CheepRepository : ICheepRepository
{
    private readonly ChatDbContext _dbContext;
    public async Task<int> CreateCheep(Cheep cheep)
    {
        
        Cheep newCheep = new() { Text = cheep.Text, Author = cheep.Author };
        var queryResult = await _dbContext.cheeps.AddAsync(newCheep); // does not write to the database!

        await _dbContext.SaveChangesAsync(); // persist the changes in the database
        return queryResult.Entity.CheepId;
    }
}