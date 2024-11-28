using System.Diagnostics;
using Chirp.Core;
using Chirp.Infrastructure.Services;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    public required string Text { get; set; }
    private readonly ICheepService _service;
    public required List<CheepDTO> Cheeps { get; set; }
    public int PageNr;
    public bool HasMorePages { get; set; }
    
    [BindProperty]
    public CheepBoxPartialModel CheepBoxPartialModel { get; set; }

    public UserTimelineModel(ICheepService service)
    {
        _service = service; 
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
        var email = User.Identity.Name;
        
        if (email != null)
        {
            // get the author dto
            var authorDto = _service.GetAuthorDTOByEmail(email);
        
            // get the text
            var text = CheepBoxPartialModel.Text;
            
            _service.CreateCheep(authorDto, text);
        
            return await Task.FromResult<IActionResult>(LocalRedirect("/" + authorDto.Name)); // it is good practice to redirect the user after a post request
        }
        return await Task.FromResult<IActionResult>(LocalRedirect("/")); // it is good practice to redirect the user after a post request

    }
    
}