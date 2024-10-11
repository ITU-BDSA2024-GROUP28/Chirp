namespace DomainModel;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }

    /*
    public Author(string authorId, string name, string email)
    {
        AuthorID = authorId;
        Name = name;
        Email = email;
    }
    */
}