namespace DomainModel;

public class AuthorDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Id { get; set; }

    public AuthorDTO(string name, string email, int id)
    {
        Name = name;
        Email = email;
        Id = id;
    }
}