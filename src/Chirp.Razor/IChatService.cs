namespace Chirp.Razor;

public interface IChatService
{
    public List<Cheep> GetCheeps();

    public List<Cheep> GetCheepsFromAuthor(Author author);
}