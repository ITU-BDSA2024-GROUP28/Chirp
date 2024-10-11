namespace DomainModel;

public interface ICheepRepository
{
    public Cheep ReadCheep(CheepDTO cheepDTO);

    public CheepDTO ReadCheep(Cheep cheep);

    public AuthorDTO createAuthor(string Author);

    public string createEmail(string AuthorName);

    public Author readAuthor(AuthorDTO authorDTO);
}