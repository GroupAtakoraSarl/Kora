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
    private KoraDbContext _dbContext;

    public CompteController(ICompteService compteService, KoraDbContext dbContext)
    {
        _compteService = compteService;
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetTransaction()
    {
        var transaction = await _compteService.GetTransaction();
        return transaction;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<CompteDto>>> GetAllComptes()
    {
        var comptes = await _compteService.GetAllComptes();
        return Ok(comptes);
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
    

    [HttpPost("CreerCompte")]
    public async Task<ActionResult<CompteDto>> AddCompte(Compte compte, int  idClient)
    {
        var newcompte = await  _compteService.AddCompte(compte, idClient);
        if (newcompte is null)
        {
            return NotFound("Client introuvable !");
        }

        var compteDto = new CompteDto
        {
            NumCompte = newcompte.NumCompte,
            Solde = newcompte.Solde,
            IdClient = idClient
        };
        
        
        return CreatedAtAction(nameof(GetAuteurByNum), new { numCompte = newcompte.NumCompte }, newcompte);
    }
    
    [HttpPost("DepotCompte")]
    public async Task<ActionResult<string>> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
    {
        var result = await _compteService.DepotCompte(numCompteExpediteur, passwordExpediteur, numCompteDestinataire, solde);
        if (!result)
            return NotFound("Compte introuvable !");

        var frais = solde * 0.05m;
        var msg = $"Dépôt effectué avec succès sur le compte {numCompteExpediteur}. Montant de frais : {frais}";
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
        var msg = $"Transfert effectué avec succès vers le numéro : {numCompte}. Frais : {frais}";
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