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
    
    [HttpGet("{numCompte}")]
    public ActionResult<CompteDto> GetAuteurByNum(string numCompte)
    {
        var compte = _compteService.GetCompteByNum(numCompte);
        if (compte is null)
        {
            return NotFound("Compte introuvable !");
        }

        return Ok(compte);
    }

    [HttpPost]
    public ActionResult<Compte> AddCompte(Compte compte)
    {
        _compteService.AddCompte(compte);
        return CreatedAtAction(nameof(GetAuteurByNum), new { idCompte = compte.IdCompte }, compte);
    }


    [HttpPost("DepotCompte")]
    public async Task<ActionResult<string>> DepotCompte(string numCompte, decimal solde)
    {
        var result = await _compteService.DepotCompte(numCompte, solde);
        if (!result)
            return NotFound("Compte introuvable !");

        var msg = $"Dépôt effectué avec succès sur le compte {numCompte}";
        return msg;
    }

    [HttpPost("RetraitCompte")]
    public async Task<ActionResult<string>> RetraitCompte(string numCompte, decimal solde)
    {
        var result = await _compteService.RetraitCompte(numCompte, solde);
        if (!result)
            return NotFound("Compte introuvable ou solde supérieure à celui du compte");
        var frais = (solde * 0.02m);
        var msg = $"Transaction effectuée avec succès. Montant de frais : {frais}";

        return msg;
    }

    [HttpPost("Transfert")]
    public async Task<ActionResult<string>> Transfert(string numCompte, decimal solde)
    {
        var result = await _compteService.Transfert(numCompte, solde);
        if (!result)
            return NotFound("Compte introuvable");
        var frais = solde * 0.02m;
        var msg = $"Transfert effectué avec succès. Frais : {frais}";
        return msg;
    }

    [HttpDelete("{numCompte}")]
    public async Task<ActionResult<string>> DeleteCompte(string numCompte)
    {
        var result = await _compteService.DeleteCompte(numCompte);
        if (!result)
            return NotFound("Compte introuvable !");
        return $"Le compte {numCompte}  bien supprimé";
    }

}