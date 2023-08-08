using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class KiosqueController : ControllerBase
{
    private readonly IKiosqueService _kiosqueService;
    private readonly IMapper _mapper;

    public KiosqueController(IKiosqueService kiosqueService, IMapper mapper)
    {
        _mapper = mapper;
        _kiosqueService = kiosqueService;
    }

    [HttpGet("GetAllKiosque")]
    public async Task<ActionResult<List<KiosqueDto>>> GetAllKiosque()
    {
        var kiosques = await _kiosqueService.GetAllKiosque();
        var kiosqueDto = _mapper.Map<List<KiosqueDto>>(kiosques);
        return Ok(kiosqueDto);
    }

    [HttpGet("GetKiosqueByAresse")]
    public async Task<ActionResult<List<KiosqueDto>>> GetKiosqueByAdresse(string adresse)
    {
        var kiosques = await _kiosqueService.GetKiosqueByAdresse(adresse);
        if (kiosques is null || kiosques.Count == 0)
        {
            return NotFound("Aucun Kiosque trouvé avec cette adresse !");
        }
        return Ok(kiosques);
    }

    [HttpPost("AddKiosque")]
    public async Task<ActionResult<Kiosque>> AddKiosque(Kiosque kiosque)
    {
        var lekiosque = await _kiosqueService.AddKiosque(kiosque);
        return Ok(lekiosque);
    }

    [HttpDelete("DeleteKiosque/{contactKiosque}")]
    public async Task<ActionResult<KiosqueDto>> DeleteKiosque(string contactKiosque)
    {
        var kiosque = await _kiosqueService.DeleteKiosque(contactKiosque);
        if (!kiosque)
        {
            NotFound("Kiosque introuvable !");
        }

        return Ok("Kiosque bien supprimé");
    }
    

}