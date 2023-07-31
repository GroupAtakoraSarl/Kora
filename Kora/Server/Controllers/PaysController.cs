using Kora.Models;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PaysController : ControllerBase
{
    private readonly PaysService _paysService;

    public PaysController(PaysService paysService)
    {
        _paysService = paysService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PaysDto>>> GetAllPays()
    {
        var pays = await _paysService.GetAllPays();
        return Ok(pays);
    }

    [HttpGet("{indicatif}")]
    public async Task<ActionResult<PaysDto>> GetPaysByIndi(int indicatif)
    {
        var pays = _paysService.GetPaysByIndi(indicatif);
        return Ok(pays);
    }

    [HttpPost("AjouterPays")]
    public async Task<ActionResult<Pays>> AddPays(Pays pays)
    {
        var newPays = await _paysService.AddPays(pays);
        return CreatedAtAction(nameof(GetPaysByIndi), new { idPays = newPays.IdPays }, newPays);
    }

}