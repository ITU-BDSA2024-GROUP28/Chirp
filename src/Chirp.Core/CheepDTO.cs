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
     * Method to turn the arguments into the values of the class
     * @param text, timestamp, author
     */
}