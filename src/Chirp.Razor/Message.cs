namespace Chirp.Razor;

public class Message
{
    public int MessageId { get; set; }
    public int UserID { get; set; }
    public string Text { get; set; }
    public User User { get; set; }
}