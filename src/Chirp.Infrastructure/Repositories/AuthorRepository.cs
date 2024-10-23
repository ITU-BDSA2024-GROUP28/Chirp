namespace DomainModel;

public class AuthorRepository : IAuthorRepository
{
    public AuthorDTO ReadAuthor(Author author)
    {
        return new AuthorDTO(author.Name, author.Email, author.AuthorId);
    }

    public Author ReadAuthor(AuthorDTO authorDTO)
    {
        throw new NotImplementedException();
    }
}