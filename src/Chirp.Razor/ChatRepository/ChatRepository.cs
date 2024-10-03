using Microsoft.EntityFrameworkCore;
using MyChat.Razor;

namespace Chirp.Razor.ChatRepository;

public class ChatRepository : IChatRepository
{
    private readonly ChatDbContext _dbContext;
    public ChatRepository(ChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Cheep>> ReadMessages(string userName)
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
}