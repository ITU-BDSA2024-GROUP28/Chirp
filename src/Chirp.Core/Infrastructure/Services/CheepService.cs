using Chirp.Core;
using Chirp.Core.Infrastructure;

namespace DomainModel;

public class CheepService : ICheepService
{
    //CheepService should take a DBFacade instance via its constructor
    
    DBFacade _facade;
    private List<CheepDTO> cheeps;
    
    public CheepService(DBFacade facade)
    {
        //injecting DBFacade
        _facade = facade;
    }

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
        cheeps = _facade.GetCheeps(pageNr);
        return cheeps;
    }

    public List<CheepDTO> GetCheepsFromAuthor(Author author, int? pageNr)
    {
        cheeps = _facade.GetCheepsFromAuthor(author, pageNr);
        return cheeps;
    }
}