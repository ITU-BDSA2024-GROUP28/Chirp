using MyChat.Razor;

namespace Chirp.Razor.CheepRepository;

public class CheepRepository : ICheepRepository
{
    private readonly ChatDbContext _dbContext;
    public async Task<int> CreateMessage(Cheep cheep)
    {
        
        Cheep newMessage = new() { Text = cheep.Text, Author = cheep.Author };
        var queryResult = await _dbContext.cheeps.AddAsync(newMessage); // does not write to the database!

        await _dbContext.SaveChangesAsync(); // persist the changes in the database
        return queryResult.Entity.CheepId;
    }
}