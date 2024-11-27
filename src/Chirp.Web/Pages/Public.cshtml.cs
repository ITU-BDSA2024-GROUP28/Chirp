using System.Security.Claims;
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
    public int pageNr;

    public PublicModel(ICheepService service, UserManager<Author> userManager)
    {
        _service = service;
        _userManager = userManager;
    }
    
    
    public ActionResult OnGet([FromQuery] int ? page)
    {
        pageNr = page ?? 1;
        Cheeps = _service.GetCheeps(pageNr);
        return Page();
    }

    public string convertTimestamp(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return dateTimeOffset.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
    }
    
    [BindProperty]
    public _CheepBoxPartialModel _cheepBoxPartialModel { get; set; }
    public async Task<IActionResult> OnPost()
    {
        if (string.IsNullOrWhiteSpace(_cheepBoxPartialModel.Text))
        {
            ModelState.AddModelError("_cheepBoxPartialModel.Text", "The message can't be empty.");
        }
        else if (_cheepBoxPartialModel.Text.Length > 160)
        {
            ModelState.AddModelError("_cheepBoxPartialModel.Text", "The message can't be longer than 160 characters");
        }

        //get author
        var email = User.Identity.Name;
        var author = _service.GetAuthorByEmail(email);
        //get timestamp
        var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        //save and use the text
        var text = _cheepBoxPartialModel.Text ?? "";
        var cheepDto = new CheepDTO
        {
            Text = text,
            Author = author.Name,
            Timestamp = timestamp,
        };
        _service.CreateCheep(cheepDto);
        
        return RedirectToPage("./UserTimeline", new {author.Name}); // it is good practice to redirect the user after a post request
    }
}