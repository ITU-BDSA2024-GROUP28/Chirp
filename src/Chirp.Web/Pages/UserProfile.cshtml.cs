using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserProfile : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly SignInManager<Author> _signInManager;
    private readonly UserManager<Author> _userManager;
    public required List<CheepDTO> CheepList;
    
    
    [BindProperty]
    public required string Email { get; set; }

    public UserProfile(ICheepService cheepService, SignInManager<Author> signInManager, UserManager<Author> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _cheepService = cheepService;
    }
    public async Task<IActionResult> OnGet([FromQuery] int? pageNumber)
    {
        Author author = (await _userManager.GetUserAsync(User) ?? throw new InvalidOperationException());

        if (author.Email != null)
        {
            Email = author.Email;
            
        }
        else
        {
            Email = "No email provided";
        }
        
        var authorName = author.UserName;
        
        if (authorName != null) CheepList = _cheepService.GetCheepsFromAuthor(authorName, pageNumber);
        
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