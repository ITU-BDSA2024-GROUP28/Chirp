using MyChat.Razor;

namespace Chirp.Razor.AuthorRepository;

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDBContext _dbContext;
    public AuthorRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> CreateAuthor(Author author)
    {
        Author newAuthor = new() { Email = author.Email, Name = author.Name };
        var queryResult = await _dbContext.authors.AddAsync(newAuthor); // does not write to the database!

        await _dbContext.SaveChangesAsync(); // persist the changes in the database
        return queryResult.Entity.AuthorId;
    }
}