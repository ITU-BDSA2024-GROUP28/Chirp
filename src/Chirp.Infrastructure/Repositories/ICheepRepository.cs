using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface ICheepRepository
{
    public CheepDTO ReadCheep(Cheep cheep);
    public void CreateCheep(Author author, String text, long timestamp);
}