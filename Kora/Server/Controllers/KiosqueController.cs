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
        if (kiosques is null)
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

    [HttpPost("EnregistrerKiosque")]
    public IActionResult EnregistrerKiosque(KiosqueSign kiosqueSign)
    {
        try
        {
            var isSucceed =
                _kiosqueService.EnregistrerKiosque(kiosqueSign.NomKiosque, kiosqueSign.CodeKiosque,
                    kiosqueSign.Password);
            if (isSucceed)
            {
                return Ok("Reussi");
            }

            return BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("ConnecterKiosque")]
    public IActionResult ConnecterKiosque(KiosqueLog kiosqueLog)
    {
        var kiosque = _kiosqueService.ConnecterKiosque(kiosqueLog.CodeKiosque, kiosqueLog.Password);
        if (kiosque != null)
        {
            return Ok(kiosque);
        }

        return NotFound();
    }

    [HttpPost("ChargeSolde")]
    public async Task<IActionResult> ChargeSolde(ChargeCompteDto chargeCompteDto)
    {
        var result = await _kiosqueService.ChargeSolde(chargeCompteDto.Solde, chargeCompteDto.ContactKiosque);
        if (result)
        {
            return Ok("Solde chargé avec succès");
        }
        else
        {
            return NotFound();
        }
    }
    
    [HttpDelete("DeleteKiosque")]
    public async Task<ActionResult<KiosqueDto>> DeleteKiosque(KiosqueDeleteDto kiosqueDeleteDto)
    {
        var kiosque = await _kiosqueService.DeleteKiosque(kiosqueDeleteDto.ContactKiosque);
        if (!kiosque)
        {
            NotFound("Kiosque introuvable !");
        }

        return Ok("Kiosque bien supprimé");
    }
    

}