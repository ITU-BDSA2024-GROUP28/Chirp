using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Services;

public class CheepService : ICheepService
{
    //Queries
    
    // add dependency to cheepdbcontext and cheep repository
    ChirpDbContext _context;
    ICheepRepository _repo;
    IAuthorRepository _repoAuthor;
    private List<CheepDTO>? _cheeps; //not initialized in constructor, but in the methods later

    public CheepService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        _context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
        DbInitializer.SeedDatabase(context);
    }

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
        // adjust the pagenr away from nullable
        int page = PageNumber(pageNr);

        // query the database to get all _cheeps to show on page
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select cheep)
            .Include(c => c.Author)
            .Skip(page * 32).Take(32);
        var result = query.ToList();

        // convert the cheep object list to cheepDTO objects
        _cheeps = new List<CheepDTO>();
        foreach (Cheep cheep in result)
        {
            _cheeps.Add(_repo.ReadCheep(cheep));
        }

        return _cheeps;
    }

    public List<CheepDTO> GetCheepsFromAuthor(string authorName, int? pageNr)
    {
        int page = PageNumber(pageNr);

        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select cheep)
            .Include(c => c.Author);
        var result = query.ToList();
        
        // convert the cheep object list to cheepDTO objects
        _cheeps = new List<CheepDTO>();
        var counter = 0;
        foreach (Cheep cheep in result)
        {
            if (cheep.Author.Name == authorName)
            {
                _cheeps.Add(_repo.ReadCheep(cheep));
                counter++;
            }
        }

        List<CheepDTO> cheepsOnPage; 
        if (counter > 32)
        {
            cheepsOnPage = new List<CheepDTO>();
            counter = 0;
            foreach (CheepDTO cheep in _cheeps)
            {
                if (counter < page * 32)
                {
                    counter++;
                }
                else if (counter < page * 32 + 32)
                {
                    cheepsOnPage.Add(cheep);
                    counter++;
                }
            }
        }
        else return _cheeps;

        Console.WriteLine("Amount of _cheeps: " + cheepsOnPage.Count);
        
        return cheepsOnPage;
        
    }
    
    public int PageNumber(int? pageNr)
    {
        int realpagenr;
        if (pageNr is null) realpagenr = 0;
        else realpagenr = pageNr.Value;
        return realpagenr;
    }
    
    //find Author by name
    public AuthorDTO GetAuthorByName(string name)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Name == name);
        if (author == null)
        {
            throw new ApplicationException("Author not found");
        }
        else
        {
            AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
            return authorDto;
        }
    }

    //find Author by email
    public AuthorDTO GetAuthorByEmail(string email)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Email == email);
        if (author == null)
        {
            throw new ApplicationException("Author not found");
        }
        else
        {
            AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
            return authorDto;
        }
    }


    //Commands
    
    //Create new Author
    public void CreateAuthor(AuthorDTO authorDto)
    {
        
    }
    
    //Create new Cheep
    public void CreateCheep(CheepDTO cheepDto)
    {
        //limit cheep length
    }
}