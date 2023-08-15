using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly IMapper _mapper;

    public ClientController(IClientService clientService, IMapper mapper)
    {
        _mapper = mapper;
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClientDto>>> GetAllClient()
    {
        var clients = await _clientService.GetAllClient();
        var clientDtos = _mapper.Map<List<ClientDto>>(clients); // Utilisez le mappage ici pour convertir les entités Client en Dto
        return Ok(clientDtos);
    }

    
    [HttpPost("Enregistrer")]
    public IActionResult EnregistrerClient(Shared.Models.Client client)
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
            int tentative = 3;

            while (tentative > 0)
            {
                var isConnected = _clientService.ConnecterClient(client.Tel, client.Password);

                if (isConnected)
                {
                    return Ok("Connexion réussie");
                }
                else
                {
                    tentative--;
                    if (tentative == 0)
                    {
                        return BadRequest(
                            "3 tentatives échouées. Veuillez contacter l'admin au '22222222' pour changer votre Mot de Passe ");
                    }
                }
            }
            
            return BadRequest("Nom d'utilisateur ou mot de passe incorrect");
        }
        catch (Exception e)
        {
            var Message = "Une erreur est survenue";
            return Ok(Message);
        }
    }
    
    [HttpPut("{tel}")]
    public async Task<IActionResult> UpdateClient(string tel, ClientDto clientDto)
    {
        var clientEntity = await _clientService.GetClientByTel(tel);
        if (clientEntity == null)
        {
            return NotFound();
        }

        _mapper.Map(clientDto, clientEntity);

        var success = await _clientService.UpateClient(tel, clientEntity);
        if (success)
        {
            return Ok("Client mis à jour avec succès");
        }
        else
        {
            return BadRequest("Échec de la mise à jour du client");
        }
    }
 

    
}