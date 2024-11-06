using Chirp.Core;
using Chirp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Services;

public class CheepService : ICheepService
{
    
    ChirpDbContext _context;
    ICheepRepository _repo;
    IAuthorRepository _repoAuthor;
    private List<CheepDTO>? _cheeps;

    public CheepService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        this._context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
    }
    /*
     * Constructor for CheepService
     * @param ChirpDbContect, ICheepRepository, IAuthorRepository
     */

    public List<CheepDTO> GetCheeps(int? pageNr)
    {
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
    /*
     * Method to retrieve get cheeps on a certain page
     * @param int (page number)
     * @return List<CheepDTO>
     */

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
    /*
     * Method to retrieve cheeps from a specific author, on a specific page
     * @param string, int
     * @return List<CheepDTO>
     */
    
    public int PageNumber(int? pageNr)
    {
        int realpagenr;
        if (pageNr is null) realpagenr = 0;
        else realpagenr = pageNr.Value;
        return realpagenr;
    }
    /*
     * Method to determine the page number
     * @param int
     * @return int
     */
    
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
    /*
     * Method to find an author by their name
     * @param string
     * @return AuthorDTO
     */

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
    /*
     * Method to find an author by their email
     * @param string
     * @return AuthorDTO
     */
    
    public void CreateAuthor(AuthorDTO authorDto)
    {
        
    }
    /*
     * Method to create an author
     * @param AuthorDTO
     */
    
    public void CreateCheep(CheepDTO cheepDto)
    {
        //limit cheep length
    }
    /*
     * Method to create a cheep
     * @param CheepDTO
     */

    public void AddCheep(Cheep cheep)
    {
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();
    }
}