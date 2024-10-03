namespace Chirp.Razor;

public interface ICheepService
{
    public List<CheepDTO> GetCheeps();

    public List<CheepDTO> GetCheepsFromAuthor(string author);
}