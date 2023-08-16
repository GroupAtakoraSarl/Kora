using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompteController : ControllerBase
{
    private readonly ICompteService _compteService;
    private readonly IMapper _mapper;

    public CompteController(IMapper mapper, ICompteService compteService, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _compteService = compteService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<List<Compte>>> GetAllComptes()
    {
        var comptes = await _compteService.GetAllComptes();
        var comptesDto = _mapper.Map<List<CompteDto>>(comptes);
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
    
    
    
    [HttpPost("Transfert")]
    public async Task<ActionResult<string>> Transfert(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
    {
        var result = await _compteService.Transfert(numCompteExpediteur, passwordExpediteur, numCompteDestinataire, solde);
        if (!result)
        {
            return NotFound("Compte introuvable !");
        }

        return Ok();
    }

    
    [HttpPost("RetraitCompte")]
    public async Task<ActionResult<string>> Retrait(string numCompte, decimal solde, string code, string password)
    {
        var result = await _compteService.Retrait(numCompte, solde, code, password);
        if (!result)
        {
            return NotFound("Compte introuvable ou solde supérieure à celui du compte");
        }
        
        return Ok();
    }

    
    [HttpPost("Depot")]
    public async Task<ActionResult<string>> Depot(string numCompte, string code, decimal solde)
    {
        var result = await _compteService.Depot(numCompte, code, solde);
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
    


}