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
    
    [BindProperty]
    public CheepBoxPartialModel CheepBoxPartialModel { get; set; }

    public UserTimelineModel(ICheepService service)
    {
        _service = service; 
        CheepBoxPartialModel = new CheepBoxPartialModel();
    }
    
    
    
    public ActionResult OnGet([FromQuery] int ? page, string author)
    {
        PageNr = page ?? 1;
        Cheeps = _service.GetCheepsFromAuthor(author, PageNr);
        return Page();
    }
    
    public string ConvertTimestamp(long timestamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
    }
}