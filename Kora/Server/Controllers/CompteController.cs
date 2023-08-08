using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompteController : ControllerBase
{
    private readonly ICompteService _compteService;
    private KoraDbContext _dbContext;
    private IMapper _mapper;

    public CompteController(IMapper mapper, ICompteService compteService, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _compteService = compteService;
        _dbContext = dbContext;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<List<Compte>>> GetAllComptes()
    {
        var comptes = await _compteService.GetAllComptes();
        var comptesDto = _mapper.Map<CompteDto>(comptes);
        return Ok(comptesDto);
    }

    [HttpGet("{numCompte}")]
    public ActionResult<CompteDto> GetCompteByNum(string numCompte)
    {
        var compte = _compteService.GetCompteByNum(numCompte);
        if (compte is null)
        {
            return NotFound("Compte introuvable !");
        }
        return Ok(compte);
    }
    

    [HttpPost("CreerCompte")]
    public async Task<ActionResult<CompteDto>> AddCompte(CompteDto compteDto)
    {
        try
        {
            var compte = _mapper.Map<Compte>(compteDto); // Effectuez le mappage ici
            var newCompte = await _compteService.AddCompte(compte);
            var newCompteDto = _mapper.Map<CompteDto>(newCompte); // Effectuez le mappage ici
            return Ok(newCompteDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    [HttpPost("DepotCompte")]
    public async Task<ActionResult<string>> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
    {
        var result = await _compteService.DepotCompte(numCompteExpediteur, passwordExpediteur, numCompteDestinataire, solde);
        if (!result)
        {
            return NotFound("Compte introuvable !");
        }

        return Ok();
    }

    
    [HttpPost("RetraitCompte")]
    public async Task<ActionResult<string>> RetraitCompte(string numCompte, decimal solde, string password)
    {
        var result = await _compteService.RetraitCompte(numCompte, solde, password);
        if (!result)
            return NotFound("Compte introuvable ou solde supérieure à celui du compte");


        return Ok();
    }

    [HttpPost("Transfert")]
    public async Task<ActionResult<string>> Transfert(string numCompte, decimal solde)
    {
        var result = await _compteService.Transfert(numCompte, solde);
        if (!result)
            return NotFound("Compte introuvable");

        return Ok();
    }
    

    [HttpDelete("{numCompte}")]
    public async Task<ActionResult<string>> DeleteCompte(string numCompte)
    {
        var result = await _compteService.DeleteCompte(numCompte);
        if (!result)
            return NotFound("Compte introuvable !");
        return $"Le compte {numCompte}  bien supprimé";
    }
    
    [HttpGet("GetTransaction")]
    public async Task<ActionResult<List<string>>> GetTransaction()
    {
        var transactions = await _compteService.GetTransaction();
        if (transactions == null || transactions.Count == 0)
        {
            return NotFound("Aucune transaction trouvée.");
        }
        return Ok(transactions);
    }

}