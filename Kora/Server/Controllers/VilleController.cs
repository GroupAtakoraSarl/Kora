using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using IVilleService = Kora.Server.Services.IVilleService;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VilleController : ControllerBase
{
    private readonly IVilleService _villeService;
    private readonly IMapper _mapper;

    public VilleController(IVilleService villeService, IMapper mapper)
    {
        _mapper = mapper;
        _villeService = villeService;
    }

    [HttpGet("GetAllVille")]
    public async Task<ActionResult<List<Ville>>> GetAllAgence()
    {
        var villes = await _villeService.GetAllVille();
        return Ok(villes);
    }

    [HttpPost("AddVille")]
    public async Task<ActionResult<VilleDto>> AddVille(VilleDto ville)
    {
        try
        {
            var laville = _mapper.Map<Ville>(ville);
            var newVille = _villeService.AddVille(laville);
            var newVilleDto = _mapper.Map<VilleDto>(newVille);
            return Ok(newVilleDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
       
    }
    
}