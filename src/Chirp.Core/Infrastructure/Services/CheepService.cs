using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace DomainModel;

public class CheepService : ICheepService
{
    //CheepService should take a DBFacade instance via its constructor
    // add dependency to cheepdbcontext
    CheepDbContext _context;
    ICheepRepository _repo;
    private List<CheepDTO> cheeps;

    public CheepService(CheepDbContext context, ICheepRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
        // adjust the pagenr away from nullable
        int page = PageNumber(pageNr);

        // query the database to get all cheeps to show on page
        var query = (from cheep in _context.Cheeps
                orderby cheep.Timestamp descending
                select cheep)
            .Include(c => c.Author)
            .Skip(page * 32).Take(32);
        var result = query.ToList();

        // convert the cheep object list to cheepDTO objects
        List<CheepDTO> cheeps = new List<CheepDTO>();
        foreach (Cheep cheep in result)
        {
            cheeps.Add(_repo.ReadCheep(cheep));
        }

        return cheeps;
    }

    public List<CheepDTO> GetCheepsFromAuthor(Author author, int? pageNr)
    {
        // adjust the pagenr away from nullable
        int page = PageNumber(pageNr);

        // query the database to get all cheeps to show on page
        var query = (from cheep in _context.Cheeps
                orderby cheep.Timestamp descending
                select cheep)
            .Where(cheep => cheep.Author == author)
            .Include(c => c.Author)
            .Skip(page * 32).Take(32);
        var result = query.ToList();

        // convert the cheep object list to cheepDTO objects
        List<CheepDTO> cheeps = new List<CheepDTO>();
        foreach (Cheep cheep in result)
        {
            cheeps.Add(_repo.ReadCheep(cheep));
        }

        return cheeps;
    }
    public int PageNumber(int? pageNr)
    {
        int realpagenr;
        if (pageNr is null) realpagenr = 1;
        else realpagenr = pageNr.Value;
        return realpagenr;
    }
}