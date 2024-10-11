using Chirp.Core;

namespace DomainModel;

public interface ICheepService
{
    public List<CheepDTO> GetCheeps(int? pageNr);
    
    public List<CheepDTO> GetCheepsFromAuthor(Author author, int? pageNr);
}