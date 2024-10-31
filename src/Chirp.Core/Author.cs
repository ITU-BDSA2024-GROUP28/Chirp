namespace Chirp.Core;

public class Author
{
    public int AuthorId { get; set; }
    //string limit CAN DO
    /*
     * AuthorId retrieves the current Author id, and sets it as the id. 
     */
    public required string Name { get; set; }
    /*
     * Name retrieves the current name, then sets it as the name
     */
    public required string Email { get; set; }
    /*
     * Email retrieves the current email, then sets it as the email
     */
    public ICollection<Cheep>? Cheeps { get; set; }
    /*
     * Cheeps retrieves the relevant cheeps from the author and sets them as the collection
     */
}