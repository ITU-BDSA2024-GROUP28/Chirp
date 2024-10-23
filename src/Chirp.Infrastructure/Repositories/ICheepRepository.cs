namespace DomainModel;

public interface ICheepRepository
{
    public Cheep ReadCheep(CheepDTO cheepDTO);

    public CheepDTO ReadCheep(Cheep cheep);

    //public AuthorDTO ReadAuthor(Author author); should be in AuthorRepository 

    //public Author ReadAuthor(AuthorDTO authorDTO); should be in AuthorRepository 
}