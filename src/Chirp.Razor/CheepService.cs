using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? pageNr);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? pageNr);
}

public class CheepService : ICheepService
{

    public List<CheepViewModel> GetCheeps(int? pageNr)
    {
        return DBFacade.GetCheeps(pageNr);
  }

    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? pageNr)
    {
        return DBFacade.GetCheepsFromAuthor(author, pageNr);
    }
}
