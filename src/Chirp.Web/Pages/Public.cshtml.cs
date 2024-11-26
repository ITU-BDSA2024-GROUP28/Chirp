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
    public int pageNr;

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    [BindProperty]
    public _CheepBoxPartialModel CheepBoxPartialModel { get; set; }
    
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
}