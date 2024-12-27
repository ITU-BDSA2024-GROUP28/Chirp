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

    /*
     * Constructor for CheepService
     * @param ChirpDbContect, ICheepRepository, IAuthorRepository
     */
    public CheepService(ChirpDbContext context, ICheepRepository repo, IAuthorRepository repoAuthor)
    {
        _context = context;
        _repo = repo;
        _repoAuthor = repoAuthor;
    }
  
    /*
     * Method to retrieve get cheeps on a certain page
     * @param int (page number)
     * @return List<CheepDTO>
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
     * Method to retrieve cheeps from a specific author, on a specific page
     * @param string, int
     * @return List<CheepDTO>
     */
    public List<CheepDTO> GetCheepsFromAuthor(string authorName, int? pageNr)
    {
        int page = PageNumber(pageNr);

        var query =  from author in _context.Authors
                where author.UserName == authorName
                select author;
        var result = query.ToList();
        
        // convert the cheep object list to cheepDTO objects
        _cheeps = new List<CheepDTO>();
        
        foreach (Author author in result)
        {
           _cheeps = author.Cheeps.Select(c => _repo.ReadCheep(c)).ToList();
        }
        
        var counter = _cheeps.Count;

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

    public bool MoreCheepsFromAuthor(string authorName, int? pageNr)
    {
        int page = PageNumber(pageNr);
        
        var CheepsCount = _context.Cheeps
            .Include(c => c.Author)
            .Count(c => c.Author.UserName == authorName);
        
        return CheepsCount >= (page + 1) * 32;
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
    
    /*
     * Method to find an author by their name
     * @param string
     * @return AuthorDTO
     */
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

    /*
     * Method to find an author by their email
     * @param string
     * @return AuthorDTO
     */
    public AuthorDTO GetAuthorDTOByEmail(string email)
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

    public Author GetAuthorByEmail(string email)
    {
        return _context.Authors.FirstOrDefault(a => a.Email == email)!;
    }
    
    /*
     * Method to create an author
     * @param AuthorDTO
     */
    public void CreateAuthor(AuthorDTO authorDto)
    {
        
    }
  
    /*
     * Method to create a cheep
     * @param CheepDTO
     */
    public void CreateCheep(AuthorDTO authorDto, String text, int CheepId)
    {
        //get timestamp
        var timestamp = DateTime.Now;
        
        // build cheep dto to send through
        var cheepDto = new CheepDTO
        {
            Text = text,
            Author = authorDto.Name,
            Timestamp = Time.ConvertToLong(timestamp),
            CheepId = CheepId
        };
        
        _repo.CreateCheep(authorDto, cheepDto);
    }
   
    /*
     * Method to add a Cheep.cs to the context
     */
    public void AddCheep(Cheep cheep)
    {
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();
    }
    
    /*
     * Method that "deletes" the author from the database
     */
    public void DeleteAuthor(string name)
    {
        _repoAuthor.DeleteAuthor(name);
    }

    public void DeleteCheep(int cheepId)
    {
        _repo.DeleteCheep(cheepId);
    }

    public List<CheepDTO> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize) //Co-authored-by: Mathias <mlao@itu.dk>
    {
        return _repo.GetCheepsFromAuthors(authors, page, pageSize).Result.ToList();
    }
}