using Chirp.Core;
using Chirp.Infrastructure.Repositories;

namespace Chirp.Infrastructure.Services;

public class CheepService : ICheepService
{
    readonly ChirpDbContext _context;
    readonly ICheepRepository _repo;
    readonly IAuthorRepository _repoAuthor;
    private List<CheepDTO>? _cheeps;

    /*
     * Constructor for CheepService with dependency injection
     * @param ChirpDbContext, ICheepRepository, IAuthorRepository
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
        
        return _repo.GetCheeps(page);
    }
   
    /*
     * Method to retrieve cheeps from a specific author, on a specific page
     * @param string, int
     * @return List<CheepDTO>
     */
    public List<CheepDTO> GetCheepsFromAuthor(string authorName, int? pageNr)
    {
        int page = PageNumber(pageNr);
        _cheeps = new List<CheepDTO>();

        Author author = _repoAuthor.ReadAuthor(authorName);
        
        // convert the cheep object list to cheepDTO objects
        _cheeps = _repo.GetCheepsFromAuthor(author);
        
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
        return _repo.CountCheepsFromAuthor(authorName, page);
    }
    
    /*
     * Method to determine the page number
     * @param int
     * @return int
     */
    private int PageNumber(int? pageNr)
    {
        var realPageNr = pageNr ?? 0;
        return realPageNr;
    }
    
    /*
     * Method to find an author by their name
     * @param string
     * @return AuthorDTO
     */
    public AuthorDTO GetAuthorByName(string name)
    {
        var author = _repoAuthor.ReadAuthor(name);
        
        if (author == null)
        {
            throw new ApplicationException("Author not found");
        }

        return _repoAuthor.ReadAuthor(author);
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
        AuthorDTO authorDto = _repoAuthor.ReadAuthor(author);
        return authorDto;
    }

    public Author GetAuthorByEmail(string email)
    {
        return _context.Authors.FirstOrDefault(a => a.Email == email)!;
    }
  
    /*
     * Method to create a cheep object
     * @param CheepDTO
     */
    public void CreateCheep(AuthorDTO authorDto, String text, int cheepId)
    {
        //get timestamp
        var timestamp = DateTime.Now;
        
        // build cheep dto to send through
        var cheepDto = new CheepDTO
        {
            Text = text,
            Author = authorDto.Name,
            Timestamp = Time.ConvertToLong(timestamp),
            CheepId = cheepId
        };
        
        _repo.CreateCheep(authorDto, cheepDto);
    }
   
    /*
     * Method to add a Cheep.cs to the context
     */
    public void AddCheep(Cheep cheep)
    {
        _repo.AddCheep(cheep);
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