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
    
    [BindProperty]
    public required string Email { get; set; }

    public UserProfile(ICheepService cheepService, IAuthorRepository authorRepository, SignInManager<Author> signInManager, UserManager<Author> userManager, ChirpDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dbContext = context;
        _cheepService = cheepService;
        _authorRepository = authorRepository;
    }
    public void OnGet()
    {
        
    }
}