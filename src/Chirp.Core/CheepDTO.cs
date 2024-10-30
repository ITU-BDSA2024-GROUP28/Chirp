namespace Chirp.Core;

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
    /*
     * CheepDTO uses the arguments text, timestamp and author
     * These are then used to set the values Text, Timestamp and Author
     */
}