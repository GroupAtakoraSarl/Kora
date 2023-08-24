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
    public async Task<IActionResult> GetAllClient()
    {
        var clients = await _clientService.GetAllClient();
        var clientsDto = _mapper.Map<List<ClientDto>>(clients);
        return Ok(clientsDto);
    }

    
    [HttpPost("Enregistrer")]
    public IActionResult EnregistrerClient([FromBody] Shared.Models.Client client)
    {
        try
        {
            var leclient = _clientService.EnregistrerClient(client);
            if (leclient != null)
            {
                return Ok("Client bien enregistré");
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }

    [HttpPost("Connecter")]
    public IActionResult ConnecterClient(ClientLog client)
    {
        try
        {
            var leclient = _clientService.ConnecterClient(client.Tel, client.Password);
            if (leclient != null)
            {
                return Ok(leclient);
            }
            
            return BadRequest("Nom d'utilisateur ou mot de passe incorrect");
        }
        catch (Exception e)
        {
            var Message = "Une erreur est survenue";
            return NotFound();
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