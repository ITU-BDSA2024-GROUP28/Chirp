namespace DomainModel;

public interface IAuthorRepository
{
    //public Author readAuthor(AuthorDTO authorDTO);
    public AuthorDTO ReadAuthor(Author author);
    
    //how to convert a DTO to a .cs
    public Author ReadAuthor(AuthorDTO authorDTO);
}

