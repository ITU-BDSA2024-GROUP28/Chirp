namespace DomainModel;

public class CheepRepository : ICheepRepository
{
    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */
    public Cheep CreateCheep(CheepDTO cheepDTO)
    {
        Author author = cheepDTO.Author;
        Cheep cheep = new Cheep(cheepDTO.Text, cheepDTO.Timestamp, author);
        return cheep;
    }
}