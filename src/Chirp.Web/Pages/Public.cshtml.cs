using System.Diagnostics;
using Chirp.Core;
using Chirp.Infrastructure.Services;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public required List<CheepDTO> Cheeps { get; set; }
    
    [BindProperty]
    public CheepBoxPartialModel CheepBoxPartialModel { get; set; }
    
    public int PageNr;

    public PublicModel(ICheepService service)
    {
        _service = service;
        CheepBoxPartialModel = new CheepBoxPartialModel();
    }
    
    
    public ActionResult OnGet([FromQuery] int ? page)
    {
        PageNr = page ?? 1;
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
        var email = User.Identity.Name;
        if (email != null)
        {
            var author = _service.GetAuthorDTOByEmail(email);
        
            //get timestamp
            var timestamp = DateTime.Now;
        
            //save and use the text
            var text = CheepBoxPartialModel.Text;
            
            var cheepdto = new CheepDTO
            {
                Text = text,
                Author = author.Name,
                Timestamp = Time.ConvertToLong(timestamp)
            };
        
            _service.CreateCheep(author, cheepdto);
        
            return await Task.FromResult<IActionResult>(LocalRedirect("/")); // it is good practice to redirect the user after a post request
        }
        return await Task.FromResult<IActionResult>(LocalRedirect("/")); // it is good practice to redirect the user after a post request

    }
}