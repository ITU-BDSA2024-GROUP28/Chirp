using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface ICheepRepository
{
    public List<CheepDTO> GetCheeps(int page);
    
    public void CreateCheep(AuthorDTO author, CheepDTO cheep);
    public void DeleteCheep(int cheepId);
    public Task<IEnumerable<CheepDTO>> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize); //Co-authored-by: Mathias <mlao@itu.dk>
    public List<CheepDTO> GetCheepsFromAuthor(Author author);
    public bool CountCheepsFromAuthor(string authorName, int page);
    
    public void AddCheep(Cheep cheep);

}