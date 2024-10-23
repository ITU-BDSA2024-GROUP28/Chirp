namespace DomainModel;

public class CheepRepository : ICheepRepository
{
    
    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */
    

    public CheepDTO ReadCheep(Cheep cheep)
    {
        return new CheepDTO(cheep.Text, Convert(cheep.TimeStamp), cheep.Author.Name);
        //returns cheep med timestamp i sec siden 1970, skal være DateTime
        //men Cheep.cs må kun bruge simple types
    }

   
    
    public Cheep ReadCheep(CheepDTO cheepDTO)
    {
        Cheep cheep = new Cheep();
        return cheep;
    }
    
    /*public Author ReadAuthor(AuthorDTO authorDTO)
    {
        Author author = new Author();
        return author;
    }*/ 

    public static long Convert(DateTime dateTime)
    {
        return((DateTimeOffset)dateTime).ToUnixTimeSeconds();
    }
}