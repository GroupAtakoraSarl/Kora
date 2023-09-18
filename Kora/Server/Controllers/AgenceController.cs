using System.Text;
using AutoMapper;
using Kora.Server.Data;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgenceController : ControllerBase
{
    private readonly IAgenceService _agenceService;
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public AgenceController(IAgenceService agenceService, IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _agenceService = agenceService;
        _dbContext = dbContext;
    }

    [HttpGet("GetAllAgence")]
    public async Task<ActionResult<List<AgenceDto>>> GetAllAgence()
    {
        var agences = await _agenceService.GetAllAgence();
        var agencesDto = _mapper.Map<List<AgenceDto>>(agences);
        return Ok(agencesDto);
    }

    [HttpGet("GetAgenceByContact/{contactAgence}")]
    public async Task<ActionResult<Agence>> GetAgenceByContact(string contactAgence)
    {
        var agence = await _agenceService.GetAgenceByContact(contactAgence);
        if (agence is null)
        {
            return NotFound("Agence introuvable");
        }

        var agenceDto = _mapper.Map<AgenceDto>(agence);

        return Ok(agenceDto);
    }

    [HttpPost("AddAgence")]
    public async Task<ActionResult<AgenceDto>> AddAgence(AgenceDto agenceDto)
    {
        try
        {
            var agence = _mapper.Map<Agence>(agenceDto);
            var newAgence = await _agenceService.AddAgence(agence);
            var newAgenceDto = _mapper.Map<AgenceDto>(newAgence);
            var respoId = _dbContext.Agences.FirstOrDefault().IdResponsable;
            var respo = _dbContext.ResponsableAgences.FirstOrDefault(r => r.IdResponsable == respoId);
            
            var smsClient = new HttpClient();
            var smsRequest = new
            {
                from = "KORA",
                to = respo.Tel,
                text = $"Bienvenue Mr/Mme, "+ respo.NomResponsable+" auprès de notre système de Transfert de Bon de Consommation KORA. Félicitation," +
                       " vous êtes maintenant le responsable de l'agence "+ agence.NomAgence ,
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }


    
    
    [HttpDelete("DeleteAgence/{contactAgence}")]
    public IActionResult DeleteAgence(string contactAgence)
    {
        var success = _agenceService.DeleteAgence(contactAgence);
        return Ok(success);
    }
    
}