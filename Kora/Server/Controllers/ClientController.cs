using Kora.Models;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAllClient()
    {
        var clients = await _clientService.GetAllClient();
        return Ok(clients);
    }

    [HttpPost("Enregistrer")]
    public IActionResult EnregistrerClient(Models.Client client)
    {
        try
        {
            _clientService.EnregistrerClient(client);
            return Ok("Enregistrement réussi");
        }
        catch (Exception e)
        {
            var Message = "Une erreure est survenue";
            return BadRequest(Message);
        }
    }
    

    [HttpPost("Connecter")]
    public IActionResult ConnecterClient(ClientLog client)
    {
        try
        {
            var isConnected = _clientService.ConnecterClient(client.Username, client.Password);
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

    
}