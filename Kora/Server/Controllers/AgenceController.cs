using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgenceController : ControllerBase
{
    private readonly IAgenceService _agenceService;
    private readonly IMapper _mapper;

    public AgenceController(IAgenceService agenceService, IMapper mapper)
    {
        _mapper = mapper;
        _agenceService = agenceService;
    }

    [HttpGet("GetAllAgence")]
    public async Task<ActionResult<List<Agence>>> GetAllAgence()
    {
        var agences = await _agenceService.GetAllAgence();
        var agencesDto = _mapper.Map<AgenceDto>(agences);
        return Ok(agencesDto);
    }

    [HttpGet("GetAgenceByContact/{contactAgence}")]
    public async Task<ActionResult<Agence>> GetAgenceByContact(string contactAgence)
    {
        var agence = await _agenceService.GetAgenceByContact(contactAgence);
        if (agence is null)
        {
            return NotFound("Agence introuvable");
        }

        var agenceDto = _mapper.Map<AgenceDto>(agence);

        return Ok(agenceDto);
    }

    [HttpPost("AjouterAgence")]
    public async Task<ActionResult<Agence>> AddAgence(Agence agence)
    {
        var newAgence = await _agenceService.AddAgence(agence);
        var newAgenceDto = _mapper.Map<AgenceDto>(newAgence);
        return Ok(newAgenceDto);
    }
    

    [HttpDelete("DeleteAgence/{contactAgence}")]
    public IActionResult DeleteAgence(string contactAgence)
    {
        var success = _agenceService.DeleteAgence(contactAgence);
        return Ok(success);
    }
    
}