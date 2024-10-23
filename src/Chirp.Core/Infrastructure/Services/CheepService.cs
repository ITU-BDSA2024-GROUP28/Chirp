using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace DomainModel;

public class CheepService : ICheepService
{
    //Queries
    
    // add dependency to cheepdbcontext and cheep repository
    CheepDbContext _context;
    ICheepRepository _repo;
    IAuthorRepository _repoAuthor;
    private List<CheepDTO> cheeps;

    public CheepService(CheepDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
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

        // query the database to get all cheeps to show on page
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
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

    public List<CheepDTO> GetCheepsFromAuthor(string authorName, int? pageNr)
    {
        int page = PageNumber(pageNr);

        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select cheep)
            .Include(c => c.Author);
        var result = query.ToList();
        
        // convert the cheep object list to cheepDTO objects
        List<CheepDTO> cheeps = new List<CheepDTO>();
        var counter = 0;
        foreach (Cheep cheep in result)
        {
            if (cheep.Author.Name == authorName)
            {
                
                    cheeps.Add(_repo.ReadCheep(cheep));
                    counter++;
                
            }
        }

        List<CheepDTO> cheepsOnPage; 
        if (counter > 32)
        {
            cheepsOnPage = new List<CheepDTO>();
            counter = 0;
            foreach (CheepDTO cheep in cheeps)
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
        else return cheeps;

        Console.WriteLine("Amount of cheeps: " + cheepsOnPage.Count);
        
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
        AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
        return authorDto;
    }
    
    //find Author by email
    public AuthorDTO GetAuthorByEmail(string email)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Email == email);
        AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
        return authorDto;
    }
    
    
    //Commands
    
    //Create new Author
    public void CreateAuthor(AuthorDTO authorDto)
    {
        
    }
    
    //Create new Cheep
    public void CreateCheep(CheepDTO cheepDto)
    {
        
    }
}