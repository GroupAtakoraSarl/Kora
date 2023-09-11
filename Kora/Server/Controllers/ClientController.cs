using System.Text;
using AutoMapper;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    public async Task<IActionResult> EnregistrerClient([FromBody] Shared.Models.Client client)
    {
        try
        {
            var leclient = _clientService.EnregistrerClient(client);
            if (leclient != null)
            {
                var smsClient = new HttpClient();
                var smsRequest = new
                {
                    from = "KORATRANSFERT",
                    to = client.Tel,
                    text = $"Bienvenue, "+ client.Username+" aupres de notre système de Transfert d'argent. Votre compte vient d'etre creer avec succès.",
                    reference = 1212,
                    api_key = "k_soGjMEHM3Te1pMfn7F3AG3WUzk3JJOAX",
                    api_secret = "s_vo70rCDvEVU8-J2FUkj6OB2rHLkg8n32"
                };

                var smsContent = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");
                var smsResponse = await smsClient.PostAsync("https://extranet.nghcorp.net/api/send-sms", smsContent);
                smsResponse.EnsureSuccessStatusCode();
                var smsResponseContent = await smsResponse.Content.ReadAsStringAsync();

                return Ok(new { Message = "Client created and SMS sent.", SmsResponse = smsResponseContent });
                
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
    
      

    
}