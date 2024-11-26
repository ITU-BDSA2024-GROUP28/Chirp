using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDbContext _context;
    
    public AuthorRepository(ChirpDbContext context)
    {
        context = _context;
    }
    
    public AuthorDTO ReadAuthor(Author author)
    {
        return new AuthorDTO
        {
            Name = author.UserName,
            Email = author.Email,
            Id = author.Id
        };
    }
    /*
     * Method that creates AuthorDTO from existing Author that EF Core uses to update database.
     * @param a Author
     * @return AuthorDTO
     */

    public Author ReadAuthor(AuthorDTO authorDTO)
    {
        throw new NotImplementedException();
    }
    /*
     * Method that creates Author from existing AuthorDTO that EF Core uses to update database.
     * @param AuthorDTO
     * @return Author
     */
    
    public Author? GetAuthorByName(string authorName)
    {
        return _context.Authors.FirstOrDefault(a => a.UserName == authorName);
    }
}