using Kora.Models;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ResponsableAgenceController : ControllerBase
{
    private readonly IResponsableAgence _responsableAgence;
  
    public ResponsableAgenceController(IResponsableAgence responsableAgence)
    {
        _responsableAgence = responsableAgence;
    }

    [HttpGet]
    public async Task<ActionResult<List<ResponsableAgenceDto>>> GetAllResponsable()
    {
        var resp = await _responsableAgence.GetAllResponsable();
        return Ok(resp);
    }

    public async Task<ActionResult<ResponsableAgenceDto>> GetResponsableByTel(string tel)
    {
        var responsable = _responsableAgence.GetResponsableByTel(tel);
        if (responsable is null)
        {
            return NotFound("Responsable introuvable !");
        }

        return Ok(responsable);
    }
    
    
    [HttpPost("AjouterResponsable")]
    public async Task<ActionResult<ResponsableAgence>> AddResponsable(ResponsableAgence responsableAgence)
    {
        var newRes = await _responsableAgence.AddResponsable(responsableAgence);
        return Ok(newRes);
    }

    [HttpDelete("{tel}")]
    public async Task<IActionResult> DeleteResponsable(string tel)
    {
        var resp = await _responsableAgence.DeleteResponsable(tel);
        if (!resp)
        {
            return NotFound("Le Responsable est introuvable !");
        }

        return NoContent();
    }

}



















