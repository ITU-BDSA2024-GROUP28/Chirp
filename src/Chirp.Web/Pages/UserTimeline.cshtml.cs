using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    public required string Text { get; set; }
    private readonly ICheepService _service;
    public required List<CheepDTO> Cheeps { get; set; }
    public int pageNr;

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
}

    public ActionResult OnGet([FromQuery] int ? page, string author)
    {
        pageNr = page ?? 1;
        Cheeps = _service.GetCheepsFromAuthor(author, pageNr);
        return Page();
    }
    
    public string convertTimestamp(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return dateTimeOffset.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
    }
}