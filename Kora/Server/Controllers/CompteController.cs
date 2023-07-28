using Kora.Models;
using Kora.Server.Data;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompteController : ControllerBase
{
    private readonly ICompteService _compteService;

    public CompteController(ICompteService compteService)
    {
        _compteService = compteService;
    }
    
    public ActionResult<CompteDto> GetAuteurByNum(string numCompte)
    {
        var compte = _compteService.GetCompteByNum(numCompte);
        if (compte == null)
            return NotFound("Compte introuvable !");
    
        return compte;
    }

    [HttpPost]
    public ActionResult<Compte> AddCompte(Compte compte)
    {
        _compteService.AddCompte(compte);
        return CreatedAtAction(nameof(GetAuteurByNum), new {})
    }

}