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
    private readonly IFollowService _followService;
    public required List<CheepDTO> Cheeps { get; set; }
    public int pageNr;
    public List<AuthorDTO> Following { get; set;}
    public List<CheepDTO> FollowCheeps { get; set; }

    public UserTimelineModel(ICheepService service)
    {
        _service = service; 
    }
    
    public _CheepBoxPartialModel CheepBoxPartialModel { get; set; }
    
    public ActionResult OnGet([FromQuery] int ? page, string author)
    {
        pageNr = page ?? 1;
        Cheeps = _service.GetCheepsFromAuthor(author, pageNr);
        FollowCheeps = _followService.GetCheepsFromFollowing(Following);
        foreach (var cheep in FollowCheeps)
        {
            Cheeps.Add(cheep);
        }
        return Page();
    }
    
    public string convertTimestamp(long timestamp)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
        return dateTimeOffset.ToLocalTime().ToString("yyyy/MM/dd HH:mm:ss");
    }
    
    /*public addAuthor(Author cheeper)
    {
        Following.add()
    }

    public removeAuthor(Author cheeper)
    {
        
    }*/
}