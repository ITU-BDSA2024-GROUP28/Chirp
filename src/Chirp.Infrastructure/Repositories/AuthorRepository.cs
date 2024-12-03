using Chirp.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly ChirpDbContext _context;
    private readonly UserManager<Author> _userManager;

    
    public AuthorRepository(ChirpDbContext context, UserManager<Author> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public AuthorDTO ReadAuthor(Author author)
    {
        return new AuthorDTO
        {
            Name = author.UserName,
            Email = author.Email,
            Id = author.Id
        };
    }
    /*
     * Method that creates AuthorDTO from existing Author that EF Core uses to update database.
     * @param a Author
     * @return AuthorDTO
     */

    public Author ReadAuthor(AuthorDTO authorDTO)
    {
        throw new NotImplementedException();
    }
    /*
     * Method that creates Author from existing AuthorDTO that EF Core uses to update database.
     * @param AuthorDTO
     * @return Author
     */

    public async Task DeleteAuthor(string name)
    {
        var author = await _context.Authors.FirstOrDefaultAsync(a => a.UserName == name);
        var user = await _userManager.FindByNameAsync(name);
        
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        
        if (author != null)
        {
            // remove author from Author table
            _context.Authors.Remove(author);
            
            // get cheeps from Cheeps table
            var cheeps = _context.Cheeps.Where(c => c.Author == author);
            _context.Cheeps.RemoveRange(cheeps);
        }
        else
        {
            Console.WriteLine($"Author {name} was not found");
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task Follow(string user, string userToFollow)
    {
        var author = _context.Authors.Where(c => c.UserName == user).FirstOrDefaultAsync();
        var authorToFollow = await _context.Authors.Where(c => c.UserName == userToFollow).FirstOrDefaultAsync();
        author.Result.Following.Add(authorToFollow);
        
        await _context.SaveChangesAsync();
    }

    public async Task Unfollow(string user, string userToFollow)
    {
        var author = _context.Authors.Where(c => c.UserName == user).FirstOrDefaultAsync();
        var authorToFollow = await _context.Authors.Where(c => c.UserName == userToFollow).FirstOrDefaultAsync();
        author.Result.Following.Remove(authorToFollow);
        
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuthorDTO>> GetUserFollowers(string user)
    {
        var author = _context.Authors
            .Where(c => c.UserName == user)
            .FirstOrDefaultAsync();
        var authorDTO = author.Result.Following.Select(a => ReadAuthor(a));
        return authorDTO;
    }
}