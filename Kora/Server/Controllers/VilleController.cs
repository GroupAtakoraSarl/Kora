using Kora.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using IVilleService = Kora.Server.Services.IVilleService;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VilleController : ControllerBase
{
    public readonly IVilleService _villeService;

    public VilleController(IVilleService villeService)
    {
        _villeService = villeService;
    }

    [HttpGet("GetAllVille")]
    public async Task<ActionResult<List<Ville>>> GetAllAgence()
    {
        var villes = await _villeService.GetAllVille();
        return Ok(villes);
    }

    [HttpPost("AddVille")]
    public async Task<ActionResult<Ville>> AddVille(Ville ville)
    {
        var laville = await _villeService.AddVille(ville);
        return Ok(laville);
    }
    
}