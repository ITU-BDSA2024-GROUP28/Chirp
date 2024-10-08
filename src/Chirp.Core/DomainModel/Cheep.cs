namespace DomainModel;

public class Cheep
{
    public int CheepID { get; set; }
    public int AuthorID { get; set; }
    public string Text { get; set; }
    public long Timestamp { get; set; }
    public Author Author { get; set; }

    public Cheep(string text, long timestamp, Author author)
    {
        Text = text;
        Timestamp = timestamp;
        Author = author;
    }
}