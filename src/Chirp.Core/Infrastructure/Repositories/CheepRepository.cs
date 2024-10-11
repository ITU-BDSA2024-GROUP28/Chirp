namespace DomainModel;

public class CheepRepository : ICheepRepository
{
    
    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */
    public Cheep ReadCheep(CheepDTO cheepDTO)
    {
        Cheep cheep = new Cheep() {Text = cheepDTO.Text, TimeStamp = DateTime.Now, Author = readAuthor(createAuthor(cheepDTO.Author))};
        return cheep;
    }

    public CheepDTO ReadCheep(Cheep cheep)
    {
        return new CheepDTO(cheep.Text, 0 , cheep.Author.Name);
    }
    
    public AuthorDTO createAuthor(string Author)
    {
        return new AuthorDTO(Author, createEmail(Author));
    }

    public string createEmail(string AuthorName)
    {
        return AuthorName + "@mail.com";
    }
    public Author readAuthor(AuthorDTO authorDTO)
    {
        Author author = new Author();
        return author;
    }
}