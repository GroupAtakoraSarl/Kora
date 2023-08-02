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

    public AgenceController(IAgenceService agenceService)
    {
        _agenceService = agenceService;
    }

    [HttpGet("GetAllAgence")]
    public async Task<ActionResult<List<AgenceDto>>> GetAllAgence()
    {
        var agences = await _agenceService.GetAllAgence();
        return Ok(agences);
    }

    [HttpGet("GetAgenceByContact/{contactAgence}")]
    public async Task<ActionResult<AgenceDto>> GetAgenceByContact(string contactAgence)
    {
        var agence = await _agenceService.GetAgenceByContact(contactAgence);
        if (agence is null)
        {
            return NotFound("Agence introuvable");
        }

        return Ok(agence);
    }

    [HttpPost("AjouterAgence")]
    public async Task<ActionResult<Agence>> AddAgence(Agence agence)
    {
        var newAgence = await _agenceService.AddAgence(agence);
        return Ok(newAgence);
    }
    

    [HttpDelete("DeleteAgence/{contactAgence}")]
    public IActionResult DeleteAgence(string contactAgence)
    {
        var success = _agenceService.DeleteAgence(contactAgence);
        return Ok(success);
    }
    
}