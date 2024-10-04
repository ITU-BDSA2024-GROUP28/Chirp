using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? pageNr);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? pageNr);
}

public class CheepService : ICheepService
{
    private readonly DBFacade _dbFacade;
    
    //CheepService takes a DBFacade instance via its constructor
    public CheepService(DBFacade dbFacade)
    {
        //injecting DBFacade
        _dbFacade = dbFacade;
    }
    
    public List<CheepViewModel> GetCheeps(int? pageNr)
    {
        return _dbFacade.GetCheeps(pageNr);
  }

    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? pageNr)
    {
        return _dbFacade.GetCheepsFromAuthor(author, pageNr);
    }
}
