using Kora.Models;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;


[ApiController]
[Route("/api/[controller]")]
public class KiosqueController : ControllerBase
{
    private readonly IKiosqueService _kiosqueService;

    public KiosqueController(IKiosqueService kiosqueService)
    {
        _kiosqueService = kiosqueService;
    }

    [HttpGet]
    public async Task<ActionResult<List<KiosqueDto>>> GetAllKiosque()
    {
        var kiosques = _kiosqueService.GetAllKiosque();
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
    public async Task<ActionResult<Kiosque>> AddKiosque(Kiosque kiosque)
    {
        var lekiosque = await _kiosqueService.AddKiosque(kiosque);
        return Ok(lekiosque);
    }

    [HttpDelete("{contactKiosque}")]
    public async Task<ActionResult<KiosqueDto>> DeleteKiosque(int contactKiosque)
    {
        var kiosque = await _kiosqueService.DeleteKiosque(contactKiosque);
        if (!kiosque)
        {
            NotFound("Kiosque introuvable !");
        }

        return Ok("Kiosque bien supprimé");
    }




}