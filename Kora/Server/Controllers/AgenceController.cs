using Kora.Models;
using Kora.Server.ModelsDto;
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

    [HttpGet]
    public async Task<ActionResult<List<AgenceDto>>> GetAllAgence()
    {
        var agences = await _agenceService.GetAllAgence();
        return Ok(agences);
    }

    [HttpGet("{contactAgence}")]
    public async Task<ActionResult<AgenceDto>> GetAgenceById(int contactAgence)
    {
        var agence = await _agenceService.GetAgenceByContact(contactAgence);
        if (agence is null)
        {
            return NotFound("Agence introuvable");
        }

        return Ok(agence);
    }

    [HttpPost]
    public async Task<ActionResult<Agence>> AddAgence(Agence agence)
    {
        var newAgence = await _agenceService.AddAgence(agence);
        return CreatedAtAction(nameof(GetAgenceById), new { idAgence = newAgence.IdAgence }, newAgence);
    }
    

    [HttpDelete("{contactAgence}")]
    public async Task<IActionResult> DeleteAgence(int contactAgence)
    {
        var success = await _agenceService.DeleteAgence(contactAgence);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
    
}