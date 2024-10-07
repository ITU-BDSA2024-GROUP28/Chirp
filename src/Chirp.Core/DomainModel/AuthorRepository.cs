namespace DomainModel;

public class AuthorRepository : IAuthorRepository
{
    public Author create(AuthorDTO authorDTO)
    {
        Author author = new Author(authorDTO.Name, authorDTO.Email);
        return author;
    }
    
}