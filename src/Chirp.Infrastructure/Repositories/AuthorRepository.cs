using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    public AuthorDTO ReadAuthor(Author author)
    {
        return new AuthorDTO(author.Name, author.Email, author.AuthorId);
    }
    /*
     * Method that creates Author from existing AuthorDTO that EF Core uses to update database.
     * @param AuthorDTO
     * @return Author
     */

    public Author ReadAuthor(AuthorDTO authorDTO)
    {
        throw new NotImplementedException();
        //Create instead, set defaults
    }
    /*
     * Method that creates AuthorDTO from existing Author that EF Core uses to update database.
     * @param a Author
     * @return AuthorDTO
     */
}