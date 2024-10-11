namespace DomainModel;

public interface ICheepRepository
{
    public Cheep ReadCheep(CheepDTO cheepDTO);

    public CheepDTO ReadCheep(Cheep cheep);

    public AuthorDTO ReadAuthor(Author author);

    public Author ReadAuthor(AuthorDTO authorDTO);
}