namespace DomainModel;

public interface IAuthorRepository
{
    public Author create(AuthorDTO authorDTO);
}