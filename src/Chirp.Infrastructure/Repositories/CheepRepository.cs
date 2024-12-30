using System.Reflection.Metadata.Ecma335;
using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Repositories;

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDbContext _context;
    
    public CheepRepository(ChirpDbContext context)
    {
        _context = context;
    }

    
    /*
     * Method that creates Cheep from existing CheepDTO that EF Core uses to update database.
     * @param a CheepDTO
     * @return Cheep object
     */
    public void CreateCheep(AuthorDTO authorDto, CheepDTO cheepdto)
    {
        var author = _context.Authors.FirstOrDefault(a => a.UserName == authorDto.Name);
        // Converts info to cheep
        if (author != null)
        {
            var cheep = new Cheep
            {
                Author = author,
                Text = cheepdto.Text,
                TimeStamp = Time.ConvertToDateTime(cheepdto.Timestamp),
                AuthorId = author.Id
            };
            _context.Cheeps.Add(cheep);
        }

        _context.SaveChanges();  // Saves the Cheep to the database
    }
    /*
     * Method to create a cheep and store it in the database
     * @param author, cheepdto
     */

    public void DeleteCheep(int cheepId)
    {
        var cheep = _context.Cheeps.Find(cheepId);
        if (cheep != null) _context.Cheeps.Remove(cheep);
        _context.SaveChanges();
    }
    /*
     * Method to delete a cheep
     * @param cheepId
     */

    public async Task<IEnumerable<CheepDTO>> GetCheepsFromAuthors(IEnumerable<string> authors, int page, int pageSize)
    {
        //Co-authored-by: Mathias <mlao@itu.dk> 
        var query = _context.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName)) //If the list of authors, contains the author of the cheep, then we want the cheep
            .Select(cheep => new {cheep.Author.UserName, cheep.CheepId, cheep.TimeStamp, cheep.Text})
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var cheeps = await query
            .Select(cheep => new CheepDTO(){Text = cheep.Text, Timestamp = Time.ConvertToLong(cheep.TimeStamp), Author = cheep.UserName, CheepId = cheep.CheepId,}).ToListAsync();
        
        return cheeps;
    }
    /*
     * A method to retrieve a chunk of cheeps made by a specific author
     * @param author, pagenr, pagesize
     */

    public List<CheepDTO> GetCheepsFromAuthor(Author author)
    {
        return author.Cheeps.Select(ReadCheep).ToList();
    }
    
    private CheepDTO ReadCheep(Cheep cheep)
    {
        return new CheepDTO
        {
            Text = cheep.Text,
            Timestamp = Time.ConvertToLong(cheep.TimeStamp),
            Author = cheep.Author.UserName,
            CheepId = cheep.CheepId,
        };
    }

    public List<CheepDTO> GetCheeps(int page)
    {
        // query the database to get all _cheeps to show on page
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select cheep)
            .Include(c => c.Author)
            .Skip(page * 32).Take(32);
        var result = query.ToList();
        
        // convert the cheep object list to cheepDTO objects
        var cheeps = new List<CheepDTO>();
        foreach (Cheep cheep in result)
        {
            cheeps.Add(ReadCheep(cheep));
        }
        
        return cheeps;
    }

    public bool CountCheepsFromAuthor(string authorName, int page)
    {
        var cheepsCount = _context.Cheeps
            .Include(c => c.Author)
            .Count(c => c.Author.UserName == authorName);
        
        return cheepsCount >= (page + 1) * 32;
    }

    public void AddCheep(Cheep cheep)
    {
        _context.Cheeps.Add(cheep);
        _context.SaveChanges();
    }
}