using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps(int? pageNr);
    public List<CheepViewModel> GetCheepsFromAuthor(string author, int? pageNr);
}

public class CheepService : ICheepService
{
    private readonly SQLDBFacade _dbFacade;

    //CheepService takes a SQLDBFacade instance via its constructor
    public CheepService(SQLDBFacade dbFacade)
    {
        //injecting SQLDBFacade
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