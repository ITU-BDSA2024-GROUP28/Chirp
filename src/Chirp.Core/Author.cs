namespace Chirp.Core;

public class Author
{
    public int AuthorId { get; set; }
    //string limit CAN DO
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ICollection<Cheep> Cheeps { get; set; }
}