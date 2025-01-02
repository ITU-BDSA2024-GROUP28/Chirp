using System.Diagnostics;
using Chirp.Core;
using Chirp.Infrastructure.Services;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    
    private readonly UserManager<Author> _userManager;
    public required List<CheepDTO> Cheeps { get; set; }
    
    [BindProperty]
    public CheepBoxPartialModel CheepBoxPartialModel { get; set; }
    
    public int PageNr;

    public PublicModel(ICheepService service, UserManager<Author> userManager)
    {
        _service = service;
        _userManager = userManager;
        CheepBoxPartialModel = new CheepBoxPartialModel();
    }
    
    public ActionResult OnGet([FromQuery] int ? page)
    {
        PageNr = page ?? 0;
        Cheeps = _service.GetCheeps(PageNr);
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
        Author author = await _userManager.GetUserAsync(User) ?? throw new InvalidOperationException();
        
        var authorName = author.UserName;
        
        if (authorName == null) throw new InvalidOperationException();
            
        var authorDto = _service.GetAuthorByName(authorName);
    
        var text = CheepBoxPartialModel.Text;
    
        // make the CheepId
        var guid = Guid.NewGuid();
        var cheepId = BitConverter.ToInt32(guid.ToByteArray(), 0);
        if (text != null) _service.CreateCheep(authorDto, text, cheepId);
        
        return await Task.FromResult<IActionResult>(LocalRedirect("/" + authorDto.Name)); // it is good practice to redirect the user after a post request
    }
}