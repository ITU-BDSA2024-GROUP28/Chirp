﻿using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDTO> Cheeps { get; set; }

    public PublicModel(ICheepService service)
    {
        _service = service;
    }   

    public ActionResult OnGet([FromQuery] int ? page)
    {
        int pageNr = page ?? 1;
        Cheeps = _service.GetCheeps(pageNr);
        return Page();
    }
}