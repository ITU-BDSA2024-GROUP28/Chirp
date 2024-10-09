namespace DomainModel;

public class AuthorRepository : IAuthorRepository
{
    /*
     * Method that creates Author from existing AuthorDTO that EF Core uses to update database.
     * @param a AuthorDTO
     * @return Author object
     */


    public Author readAuthor(AuthorDTO authorDTO)
    {
        throw new NotImplementedException();
    }
}