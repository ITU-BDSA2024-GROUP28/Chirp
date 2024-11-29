using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.Repositories;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserProfile : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly IAuthorRepository _authorRepository;
    private readonly SignInManager<Author> _signInManager;
    private readonly UserManager<Author> _userManager;
    private readonly ChirpDbContext _dbContext;
    public required List<CheepDTO> CheepList;
    
    [BindProperty]
    public string Email { get; set; }

    public UserProfile(ICheepService cheepService, IAuthorRepository authorRepository, SignInManager<Author> signInManager, UserManager<Author> userManager, ChirpDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = context;
        _cheepService = cheepService;
        _authorRepository = authorRepository;
    }
    public async Task<IActionResult> OnGet([FromQuery] int? pageNumber)
    {
        Author author = await _userManager.GetUserAsync(User);
        
        Email = author.Email;

        var authorName = author.UserName;
        
        CheepList = _cheepService.GetCheepsFromAuthor(authorName, pageNumber);
        
        return Page();
    }

    public async Task<IActionResult> OnPostDelete(string name)
    {
        //use DeleteAuthor method in cheepService to delete author (and maybe cheeps (not necessarily))
        _cheepService.DeleteAuthor(name);
        //sign out 
        await _signInManager.SignOutAsync();
        //redirect
        return await Task.FromResult<IActionResult>(LocalRedirect("/"));    
    }

    public async Task<IActionResult> OnPostDeleteCheep(int cheepId)
    {
        _cheepService.DeleteCheep(cheepId);
        return await Task.FromResult<IActionResult>(LocalRedirect("/"));    
    }
}