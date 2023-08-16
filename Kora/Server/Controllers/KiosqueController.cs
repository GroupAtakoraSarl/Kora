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
    public async Task<ActionResult<List<Kiosque>>> GetAllKiosque()
    {
        var kiosques = await _kiosqueService.GetAllKiosque();
        return Ok(kiosques);
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
    public async Task<ActionResult<KiosqueDto>> AddKiosque(KiosqueDto kiosqueDto)
    {
        try
        {
            var kiosque = _mapper.Map<Kiosque>(kiosqueDto);
            var newKiosque = await _kiosqueService.AddKiosque(kiosque);
            var newKiosqueDto = _mapper.Map<KiosqueDto>(newKiosque);
            return Ok(newKiosqueDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    [HttpPost("ChargeSolde")]
    public async Task<IActionResult> ChargeSolde(string contactKiosque, decimal solde)
    {
        var result = await _kiosqueService.ChargeSolde(solde, contactKiosque);
        if (result)
        {
            return Ok("Solde chargé avec succès");
        }
        else
        {
            return NotFound();
        }
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