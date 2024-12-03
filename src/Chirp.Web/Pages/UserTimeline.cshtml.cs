using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Chirp.Core;
using Chirp.Infrastructure.Services;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    private readonly IFollowService _followservice;
    private readonly UserManager<Author> _userManager;
    public required List<CheepDTO> Cheeps { get; set; }
    public int PageNr;
    public bool HasMorePages { get; set; }
    
    [BindProperty]
    public CheepBoxPartialModel CheepBoxPartialModel { get; set; }

    public UserTimelineModel(ICheepService service, UserManager<Author> userManager, IFollowService followService)
    {
        _service = service; 
        _userManager = userManager;
        _followservice = followService;
        
        CheepBoxPartialModel = new CheepBoxPartialModel();
    }
    
    public ActionResult OnGet([FromQuery] int ? page, string author)
    {
        var follows = _followservice
            .GetFollowing(User.Identity.Name) //Gets list of followed authors
            .Select(a => a.Name).ToList(); //Remaps the AuthorDTOs to their names
        follows.Add(author); //Adding the user, so the user can see their own cheeps
        
        PageNr = page ?? 0;
        Cheeps = _service.GetCheepsFromAuthors(follows, PageNr, 32);
        HasMorePages = _service.MoreCheepsFromAuthor(author, PageNr);
        return Page();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (string.IsNullOrWhiteSpace(CheepBoxPartialModel.Text))
        {
            ModelState.AddModelError("_cheepBoxPartialModel.Text", "The message can't be empty.");
        }
        else if (CheepBoxPartialModel.Text.Length > 160)
        {
            ModelState.AddModelError("_cheepBoxPartialModel.Text", "The message can't be longer than 160 characters");
        }

        //get author
        Debug.Assert(User.Identity != null, "User.Identity != null");
        
        // get author email
        var author = await _userManager.GetUserAsync(User);

        // get the author dto
        var authorDto = _service.GetAuthorDTOByEmail(author.Email);
    
        // get the text
        var text = CheepBoxPartialModel.Text;
        
        // make the CheepId
        var guid = Guid.NewGuid();
        var cheepId = BitConverter.ToInt32(guid.ToByteArray(), 0);
        
        _service.CreateCheep(authorDto, text, cheepId);
    
        return await Task.FromResult<IActionResult>(LocalRedirect("/" + authorDto.Name)); // it is good practice to redirect the user after a post request
        
    }
    
    public async Task<IActionResult> OnPostFollow(string userToFollow) 
    {
        _followservice.Follow(User.Identity.Name, userToFollow);
        return RedirectToPage(null);
    }
    
    public async Task<IActionResult> OnPostUnfollow(string userToUnfollow) 
    {
        _followservice.Unfollow(User.Identity.Name, userToUnfollow);
        return RedirectToPage(null);
    }

    
    public bool CheckIfFollowing(string userToFollow)
    {
        if (userToFollow == null || User.Identity.Name == null) throw new ArgumentNullException();
        var follows = _followservice
            .GetFollowing(User.Identity.Name) //Gets list of followed authors
            .Select(a => a.Name).ToList(); //Remaps the AuthorDTOs to their names
            
        return follows.Contains(userToFollow); //Checks if the desired author is in the list
    }
}