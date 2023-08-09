using AutoMapper;
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
    private readonly IMapper _mapper;

    public PaysController(IPaysService paysService, IMapper mapper)
    {
        _mapper = mapper;
        _paysService = paysService;
    }

    [HttpGet("GetAllPays")]
    public async Task<ActionResult<List<Pays>>> GetAllPays()
    {
        var pays = await _paysService.GetAllPays();
        return Ok(pays);
    }


    [HttpPost("AddPays")]
    public async Task<ActionResult<PaysDto>> AddPays(PaysDto pays)
    {
        var lepays = _mapper.Map<Pays>(pays);
        var newPays = await _paysService.AddPays(lepays);
        return Ok(newPays);
    }

}