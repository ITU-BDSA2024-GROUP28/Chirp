namespace Chirp.Core;

public class AuthorDTO
{
    public string Name { get; set; }
    /**
     * Name retrieves the name, then sets it as the name
     */
    public string Email { get; set; }
    /**
     * Email retrieves the email, then sets it as the email
     */
    public int Id { get; set; }
    /**
     * Id retrieves the Author id and sets it as the id.
     */
    public AuthorDTO(string name, string email, int id)
    {
        Name = name;
        Email = email;
        Id = id;
    }
    /**
     * AuthorDTO takes the arguments name, email and id
     * These are used to set the arguments as the values of Name, Email and Id
     */
}