namespace Chirp.Razor;

public class Cheep
{
    public int CheepId { get; set; }
    public int AuthorID { get; set; }
    public string Text { get; set; }
    public int TimeStamp { get; set; }
    public Author Author { get; set; }
}