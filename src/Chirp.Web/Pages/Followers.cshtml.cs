using Chirp.Core;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class Followers : PageModel
{
    private readonly IFollowService _followService;
    public List<AuthorDTO> ListOfFollowers;
    
    public Followers(IFollowService followService)
    {
        _followService = followService;
    }
    public void OnGet()
    {
        ListOfFollowers = _followService.GetFollowers(User.Identity.Name);
    }
}