namespace DomainModel;

public class Cheep
{
    public int CheepID { get; set; }
    public int AuthorID { get; set; }
    public string Text { get; set; }
    public long Timestamp { get; set; }
    public Author Author { get; set; }
}