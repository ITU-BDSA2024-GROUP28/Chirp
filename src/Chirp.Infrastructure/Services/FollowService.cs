using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Services;

public class FollowService : IFollowService
{
    
    ChirpDbContext _context;
    ICheepRepository _repo;
    IAuthorRepository _repoAuthor;
    private List<CheepDTO>? _cheeps;
    private List<AuthorDTO> _following;

    /*
     * Constructor for CheepService
     * @param ChirpDbContect, ICheepRepository, IAuthorRepository
     */
    public FollowService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        _context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
        _following = ;
    }
    
    public List<CheepDTO> GetCheepsFromAuthor(string author, int? pageNr)
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
            if (cheep.Author.UserName == author)
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

    public AuthorDTO GetAuthorByName(string name)
    {
        var author = _context.Authors.FirstOrDefault(a => a.UserName == name);
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

    public List<AuthorDTO> GetFollowing(string author)
    {
        throw new NotImplementedException();
    }

    public void Follow(string author)
    {
        throw new NotImplementedException();
    }

    public void Unfollow(string author)
    {
        throw new NotImplementedException();
    }
    
    /*
     * Method to determine the page number
     * @param int
     * @return int
     */
    public int PageNumber(int? pageNr)
    {
        int realpagenr;
        if (pageNr is null) realpagenr = 0;
        else realpagenr = pageNr.Value;
        return realpagenr;
    }
}