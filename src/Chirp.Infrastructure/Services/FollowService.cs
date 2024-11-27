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
    public List<CheepDTO> _cheepsFromFollowing;

    /*
     * Constructor for CheepService
     * @param ChirpDbContect, ICheepRepository, IAuthorRepository
     */
    public FollowService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        _context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
        _following = new List<AuthorDTO>();
        _cheepsFromFollowing = new List<CheepDTO>();
    }
    
    public List<CheepDTO> GetCheepsFromAuthor(string author)
    {
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
        
        return _cheeps;
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

    public List<AuthorDTO> GetFollowing()
    {
        return _following;
    }

    public void Follow(string author)
    {
        _following.Add(GetAuthorByName(author));
    }

    public void Unfollow(string author)
    {
        _following.Remove(GetAuthorByName(author));
    }

    public List<CheepDTO> GetCheepsFromFollowing(string author)
    {
        foreach (var authorName in _following)
        {
            List<CheepDTO> temp = GetCheepsFromAuthor(author);
            foreach (var cheep in temp)
            {
                _cheepsFromFollowing.Add(cheep);
            }
        }
        return _cheepsFromFollowing;
    }

}