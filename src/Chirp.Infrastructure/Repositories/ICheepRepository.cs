using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface ICheepRepository
{
    public CheepDTO ReadCheep(Cheep cheep);
    public void CreateCheep(AuthorDTO author, CheepDTO cheep);
    public void DeleteCheep(int cheepId);
    public Task<IEnumerable<CheepDTO>> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize); //Co-authored-by: Mathias <mlao@itu.dk>
}