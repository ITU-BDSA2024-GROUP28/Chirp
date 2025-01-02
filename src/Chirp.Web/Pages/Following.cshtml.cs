using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class Following : PageModel
{
    private readonly IFollowService _followService;
    public required List<AuthorDTO> ListOfFollowing;

    public Following (IFollowService followService)
    {
        _followService = followService;
    }
    public void OnGet()
    {
        if (User.Identity != null)
            if (User.Identity.Name != null)
                ListOfFollowing = _followService.GetFollowing(User.Identity.Name);
    }
}