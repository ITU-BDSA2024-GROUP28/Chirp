using Chirp.Core;

namespace DomainModel;

public class CheepService : ICheepService
{
    

    //CheepService should take a DBFacade instance via its constructor
    public CheepService()
    {
        //injecting SQLDBFacade
    }

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
        var cheeps = new List<CheepDTO>();
        //cheeps.Add(cheep);
        return cheeps;
    }

    public List<CheepDTO> GetCheepsFromAuthor(Author author, int? pageNr)
    {
        var cheeps = new List<CheepDTO>();
        //cheeps.Add(cheep);
        return cheeps;
    }
}