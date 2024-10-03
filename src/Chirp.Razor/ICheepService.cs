namespace Chirp.Razor;

public interface ICheepService
{
    public List<Cheep> GetCheeps();

    public List<Cheep> GetCheepsFromAuthor(Author author);
}