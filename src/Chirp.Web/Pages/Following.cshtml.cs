using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class Following : PageModel
{
    private readonly IFollowService _followService;
    public List<AuthorDTO> ListOfFollowing;

    public Following (IFollowService followService)
    {
        _followService = followService;
    }
    public void OnGet()
    {
        ListOfFollowing = _followService.GetFollowing(User.Identity.Name);
    }
}