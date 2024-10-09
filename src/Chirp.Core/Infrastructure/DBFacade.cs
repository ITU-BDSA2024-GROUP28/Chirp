using DomainModel;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Core.Infrastructure;

public class DBFacade
{
    // add dependency to cheepdbcontext
    CheepDbContext _context;
    CheepRepository _repo;
    private List<CheepDTO> cheeps;

    public DBFacade(CheepDbContext context, CheepRepository repository)
    {
        _context = context;
        _repo = repository;
    }

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
        int realpagenr;
        if (pageNr is null) realpagenr = 1;
        else realpagenr = pageNr.Value;

        var query = (from cheep in _context.Cheeps
                orderby cheep.Timestamp descending
                select cheep)
            .Include(c => c.Author)
            .Skip(realpagenr * 32).Take(32);
        var result = query.ToList();

        List<CheepDTO> cheeps = new List<CheepDTO>();
        foreach (Cheep cheep in result)
        {
            cheeps.Add(_repo.ReadCheep(cheep));
        }
        // convert cheep to cheep dto

        return cheeps;
    }

    public List<CheepDTO> GetCheepsFromAuthor(Author author, int? pageNr)
    {
        var cheeps = new List<CheepDTO>();
        //cheeps.Add(cheep);
        return cheeps;
    }
}