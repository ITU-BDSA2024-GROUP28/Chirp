using Chirp.Core;

namespace Chirp.Infrastructure.Repositories;

public interface ICheepRepository
{
    public CheepDTO ReadCheep(Cheep cheep);
    public void CreateCheep(AuthorDTO author, CheepDTO cheep);
}