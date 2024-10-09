using DomainModel;

namespace Chirp.Core.Infrastructure;

public class DBFacade
{
    
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