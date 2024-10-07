namespace DomainModel;

public class Author
{
    public int AuthorID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }

    public Author(string name, string email)
    {
        Name = name;
        Email = email;
    }
}