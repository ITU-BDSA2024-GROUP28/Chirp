namespace DomainModel;

public class CheepDTO
{
    public string Text;
    public long Timestamp;
    public string Author;

    public CheepDTO(string text, long timestamp, string author)
    {
        Text = text;
        Timestamp = timestamp;
        Author = author;
    }
}