namespace DomainModel;

public class CheepDTO
{
    public string Text;
    public long Timestamp;
    public Author Author;

    public CheepDTO(string text, long timestamp, Author author)
    {
        Text = text;
        Timestamp = timestamp;
        Author = author;
    }
}