using AutoMapper;
using Kora.Server.Data;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IVilleService = Kora.Server.Services.IVilleService;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VilleController : ControllerBase
{
    private readonly IVilleService _villeService;
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public VilleController(KoraDbContext dbContext, IVilleService villeService, IMapper mapper)
    {
        _mapper = mapper;
        _villeService = villeService;
        _dbContext = dbContext;
    }

    [HttpGet("GetAllVille")]
    public async Task<ActionResult<IEnumerable<Ville>>> GetAllVille()
    {
        var villes = await _villeService.GetAllVille();
        return Ok(villes);
    }
    
    [HttpGet("GetAllVilleWithPays")]
    public async Task<ActionResult<IEnumerable<Ville>>> GetAllVilleWithPays()
    {
        var villes = await _villeService.GetAllVilleWithPays();
        return Ok(villes);
    }
    
    
    [HttpPost("AddVille")]
    public async Task<ActionResult<VilleDto>> AddVille(VilleDto ville)
    {
        var laville = _mapper.Map<Ville>(ville);
        var newVille = _villeService.AddVille(laville);
        return Ok(newVille);
    }
}
