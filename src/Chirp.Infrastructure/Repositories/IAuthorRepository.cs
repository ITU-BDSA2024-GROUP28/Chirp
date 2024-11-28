using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface IAuthorRepository
{
    public AuthorDTO ReadAuthor(Author author);
    
    public Author ReadAuthor(AuthorDTO authorDTO);
    
    public Task DeleteAuthor(string name);
}

