using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministrateurController : ControllerBase
{
    private readonly IAdministrateurService _administrateurService;
    private readonly IMapper _mapper;

    public AdministrateurController(IAdministrateurService administrateurService, IMapper mapper)
    {
        _mapper = mapper;
        _administrateurService = administrateurService;
    }

    [HttpGet]
    public async Task<ActionResult<List<AdministrateurDto>>> GetAllAdmin()
    {
        var admins = await _administrateurService.GetAllAdmin();
        var adminDto = _mapper.Map<List<AdministrateurDto>>(admins);
        return Ok(adminDto);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<AdministrateurDto>> GetAdminByEmail(string email)
    {
        var admin = await _administrateurService.GetAdminByEmail(email);
        if (admin is null)
        {
            return NotFound("Admin introuvable");
        }

        var adminDto = _mapper.Map<AdministrateurDto>(admin);
        return Ok(adminDto);
    }

    [HttpPost("EnregistrerAdmin")]
    public IActionResult EnregistrerAdmin(Administrateur administrateur)
    {
        try
        {
            _administrateurService.EnregistrerAdmin(administrateur);
            return Ok("Enregistrement réussi");
        }
        catch (Exception e)
        {
            var Message = "Une erreure est survenue";
            return BadRequest(Message);
        }
    }
    
    [HttpPost("ConnecterAdmin")]
    public IActionResult ConnecterAdmin(AdministrateurLog administrateur)
    {
        try
        {
            var isConnected = _administrateurService.ConnecterAdmin(administrateur.Email, administrateur.Password);
            if (!isConnected)
            {
                return BadRequest("Nom d'utilisateur ou mot de passe incorrect");
            }
            else
            {
                return Ok("Connexion réussie");
            }
        }
        catch (Exception e)
        {
            var Message = "Une erreur est survenue";
            return Ok(Message);
        }
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> DeleteAdmin(string email)
    {
        var success = await _administrateurService.DeleteAdmin(email);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

}