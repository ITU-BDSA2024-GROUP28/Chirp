namespace Chirp.Core;

public class AuthorDTO
{
    public required string Name { get; set; }
    /*
     * Name retrieves the current name, then sets it as the name
     */
    public required string Email { get; set; }
    /*
     * Email retrieves the current email, then sets it as the email
     */
    public int Id { get; set; }
    /*
     * Id retrieves the current id and sets it as the id.
     */
    
    
    /*
     * Method to turn the arguments into the values of the class
     * @param name, email, id
     */
}