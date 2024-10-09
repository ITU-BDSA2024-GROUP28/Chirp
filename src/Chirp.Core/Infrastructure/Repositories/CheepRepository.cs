namespace DomainModel;

public class CheepRepository : ICheepRepository
{
    public Cheep CreateCheep(CheepDTO cheepDTO)
    {
        Author author = cheepDTO.Author;
        Cheep cheep = new Cheep(cheepDTO.Text, cheepDTO.Timestamp, author);
        return cheep;
    }
}