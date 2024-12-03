using System.Diagnostics;
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

    public UserTimelineModel(ICheepService service, IFollowService followService, UserManager<Author> userManager)
    {
        _service = service; 
        _followservice = followService;
        _userManager = userManager;
        
        CheepBoxPartialModel = new CheepBoxPartialModel();
    }
    
    public ActionResult OnGet([FromQuery] int ? page, string author)
    {
        PageNr = page ?? 0;
        Cheeps = _service.GetCheepsFromAuthor(author, PageNr);
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
    
    public async Task<IActionResult> Follow(string cheeper) 
    {
        _followservice.Follow(cheeper);
        return null;
    }
    
    public async Task<IActionResult> Unfollow(string cheeper) 
    {
        _followservice.Follow(cheeper);
        return null;
    }
   
}