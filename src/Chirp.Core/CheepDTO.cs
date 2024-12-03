namespace Chirp.Core;

public class CheepDTO
{
    public required string Author { get; set; }
    public required string Text { get; set; }
    public required long Timestamp { get; set; }
    public required long CheepId { get; set; }
    
    /*
     * Method to turn the arguments into the values of the class
     * @param text, timestamp, author
     */
}