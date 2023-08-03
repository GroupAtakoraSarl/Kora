using Kora.Server.Services;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PaysController : ControllerBase
{
    private readonly IPaysService _paysService;

    public PaysController(IPaysService paysService)
    {
        _paysService = paysService;
    }

    [HttpGet("GetAllPays")]
    public async Task<ActionResult<List<PaysDto>>> GetAllPays()
    {
        var pays = await _paysService.GetAllPays();
        return Ok(pays);
    }


    [HttpPost("AddPays")]
    public async Task<ActionResult<Pays>> AddPays(Pays pays)
    {
        var newPays = await _paysService.AddPays(pays);
        return Ok(newPays);
    }

}