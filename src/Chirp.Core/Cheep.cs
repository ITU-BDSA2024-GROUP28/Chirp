namespace Chirp.Core;

public class Cheep
{
    public int CheepId { get; set; }
    /*
     * CheepId retrieves the id of the current cheep, and sets it as the value
     */
    public int AuthorId { get; set; }
    /*
     * AuthorId retrieves the id of the current author, and sets it as the value
     */
    public required string Text { get; set; }
    /*
     * Text retrieves the text of the current cheep, and sets it as the current value
     */
    public DateTime TimeStamp { get; set; }
    /*
     * TimeStamp retrieves the timestamp of the current cheep, and sets it as the value
     */
    public required Author Author { get; set; }
    /*
     * Author retrieves the author of the current cheep, ans sets it as the value
     */
}