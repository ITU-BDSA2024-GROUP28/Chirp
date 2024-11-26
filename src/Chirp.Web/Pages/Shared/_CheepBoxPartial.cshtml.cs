using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class _CheepBoxPartialModel
{
    [BindProperty]
    [Microsoft.Build.Framework.Required]
    [StringLength(160, ErrorMessage = "Maximum length is {1}")]
    [Display(Name = "Message Text")]

    public string Text { get; set; }
}