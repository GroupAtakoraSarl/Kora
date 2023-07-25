using Kora.Models;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(ClientService clientService)
    {
        _clientService = clientService;
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