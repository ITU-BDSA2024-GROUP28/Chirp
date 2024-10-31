namespace Chirp.Core;

public class AuthorDTO
{
    public string Name { get; set; }
    /*
     * Name retrieves the current name, then sets it as the name
     */
    public string Email { get; set; }
    /*
     * Email retrieves the current email, then sets it as the email
     */
    public int Id { get; set; }
    /*
     * Id retrieves the current id and sets it as the id.
     */
    public AuthorDTO(string name, string email, int id)
    {
        Name = name;
        Email = email;
        Id = id;
    }
    /*
     * Method to turn the arguments into the values of the class
     * @param name, email, id
     */
}