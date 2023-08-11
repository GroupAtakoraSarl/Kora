using AutoMapper;
using Kora.Server.Data;
using Kora.Server.Services;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PaysController : ControllerBase
{
    private readonly IPaysService _paysService;
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public PaysController(KoraDbContext dbContext, IPaysService paysService, IMapper mapper)
    {
        _mapper = mapper;
        _paysService = paysService;
        _dbContext = dbContext;

    }

    [HttpGet("GetAllPays")]
    public async Task<ActionResult<IEnumerable<Pays>>> GetAllPays()
    {
        var pays = await _dbContext.Pays.ToListAsync();
        return Ok(pays);
    }

    [HttpGet("GetPaysNameById")]
    public async Task<ActionResult<Pays>> GetPaysNameById(int idPays)
    {
        var lepays = _paysService.GetPaysNameById(idPays);
        return Ok(lepays);
    }
    
    
    [HttpPost("AddPays")]
    public async Task<ActionResult<PaysDto>> AddPays(PaysDto pays)
    {
        var lepays = _mapper.Map<Pays>(pays);
        var newPays = await _paysService.AddPays(lepays);
        return Ok(newPays);
    }

}