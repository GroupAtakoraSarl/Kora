using System.Text;
using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Kora.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Kora.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompteController : ControllerBase
{
    private readonly ICompteService _compteService;
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;
    private readonly ITransactionService _transactionService;

    public CompteController(IMapper mapper, ICompteService compteService, KoraDbContext dbContext, ITransactionService transactionService)
    {
        _mapper = mapper;
        _compteService = compteService;
        _dbContext = dbContext;
        _transactionService = transactionService;
    }
    
    
    [HttpGet]
    public async Task<ActionResult<List<Compte>>> GetAllComptes()
    {
        var comptes = await _compteService.GetAllComptes();
        var comptesDto = _mapper.Map<List<CompteDto>>(comptes);
        return Ok(comptesDto);
    }

    [HttpGet("{numCompte}")]
    public ActionResult<CompteDto> GetCompteByNum(string numCompte)
    {
        var compte = _compteService.GetCompteByNum(numCompte);
        if (compte is null)
        {
            return NotFound("Compte introuvable !");
        }
        return Ok(compte);
    }
    
    
    [HttpPost("Transfert")]
    public async Task<ActionResult<string>> Transfert(TransfertDto transfertDto)
    {
        var result = _compteService.Transfert(transfertDto.NumCompteExpediteur, transfertDto.PasswordExpediteur, transfertDto.NumCompteDestinataire, transfertDto.Solde);
        var Exp = _dbContext.Clients.FirstOrDefault(c => c.Tel == transfertDto.NumCompteExpediteur);
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == Exp.Tel);
        var transac = _dbContext.Transactions
            .Where(t => t.IdCompte == compte.IdCompte)
            .OrderByDescending(t => t.IdTransaction)
            .FirstOrDefault();
        
        if (result is null)
        {
            return NotFound("Compte introuvable !");
        }

        var smsClient = new HttpClient();
        
        var smsRequest = new
        {
            from = "KORA",
            to = Exp.Tel,
            text = $"Vous avez effectué un transfert de "+ transfertDto.Solde +
                   " KORA équivalents à "+ (transfertDto.Solde * 500m) +" FCFA vers le numéro de compte "+
                   transfertDto.NumCompteDestinataire +",le "+ DateTime.Now +". Frais: "+ transac.Frais+" FCFA. Nouveau solde: "+
                   compte.Solde+" KORA solt "+ (compte.Solde * 500m)+" FCFA. Découvrez la nouvelle application KORA: PlayStore ...",
            reference = 1212,
            api_key = "k_soGjMEHM3Te1pMfn7F3AG3WUzk3JJOAX",
            api_secret = "s_vo70rCDvEVU8-J2FUkj6OB2rHLkg8n32"
        };

        var smsContent = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");
        var smsResponse = await smsClient.PostAsync("https://extranet.nghcorp.net/api/send-sms", smsContent);
        smsResponse.EnsureSuccessStatusCode();
        var smsResponseContent = await smsResponse.Content.ReadAsStringAsync();

        return Ok(new { Message = "Success transfert and SMS sent.", SmsResponse = smsResponseContent });
    }

    
    [HttpPost("RetraitCompte")]
    public async Task<ActionResult<string>> Retrait(RetraitDto retraitDto)
    {
        var result = _compteService.Retrait(retraitDto.NumCompte, retraitDto.Solde, retraitDto.Code, retraitDto.Password);
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == retraitDto.NumCompte);
        var transac = _dbContext.Transactions
            .Where(t => t.IdCompte == compte.IdCompte)
            .OrderByDescending(t => t.IdTransaction)
            .FirstOrDefault();
        
        if (result != null)
        {
            var smsClient = new HttpClient();
            var smsRequest = new
            {
                from = "KORA",
                to = compte.NumCompte,
                text = $"Vous avez effectué un retrait de "+ retraitDto.Solde +
                       " KORA équivalents à "+ (retraitDto.Solde * 500m) +" FCFA, le "+
                       DateTime.Now +". Frais: "+ transac.Frais+" FCFA. Nouveau solde: "+
                       compte.Solde+" KORA solt "+ (compte.Solde * 500m)+" FCFA. Découvrez la nouvelle application KORA: PlayStore ...",
                reference = 1212,
                api_key = "k_soGjMEHM3Te1pMfn7F3AG3WUzk3JJOAX",
                api_secret = "s_vo70rCDvEVU8-J2FUkj6OB2rHLkg8n32"
            };

            var smsContent = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");
            var smsResponse = await smsClient.PostAsync("https://extranet.nghcorp.net/api/send-sms", smsContent);
            smsResponse.EnsureSuccessStatusCode();
            var smsResponseContent = await smsResponse.Content.ReadAsStringAsync();

            return Ok(new { Message = "Success transfert and SMS sent.", SmsResponse = smsResponseContent });

        }
        
        return NotFound();
    }
    
    [HttpPost("Depot")]
    public async Task<ActionResult<string>> Depot(DepotDto depotDto)
    {
        var result = _compteService.Depot(depotDto.NumCompte, depotDto.Code, depotDto.Solde);
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Tel == depotDto.NumCompte);
        var lekiosque = _dbContext.Kiosques.FirstOrDefault(k => k.Code == depotDto.Code);
        var lecompte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == leclient.Tel);
        if (result != null)
        {
            var smsClient = new HttpClient();
        
            var smsRequest = new
            {
                from = "KORA",
                to = leclient.Tel,
                text = $"Vous avez effectué un dépôt de "+ (depotDto.Solde / 500m) +
                       " KORA équivalents à "+ (depotDto.Solde) +" FCFA chez le kiosque "+
                       lekiosque.NomKiosque+"("+lekiosque.Code+ "), le "+ DateTime.Now +". Frais: 0 FCFA. Nouveau solde: "+
                       lecompte.Solde+" KORA solt "+ (lecompte.Solde * 500m)+" FCFA. Découvrez la nouvelle application KORA: PlayStore ...",
                reference = 1212,
                api_key = "k_soGjMEHM3Te1pMfn7F3AG3WUzk3JJOAX",
                api_secret = "s_vo70rCDvEVU8-J2FUkj6OB2rHLkg8n32"
            };

            var smsContent = new StringContent(JsonConvert.SerializeObject(smsRequest), Encoding.UTF8, "application/json");
            var smsResponse = await smsClient.PostAsync("https://extranet.nghcorp.net/api/send-sms", smsContent);
            smsResponse.EnsureSuccessStatusCode();
            var smsResponseContent = await smsResponse.Content.ReadAsStringAsync();

            return Ok(new { Message = "Success transfert and SMS sent.", SmsResponse = smsResponseContent });

        }
        
        return NotFound("Solde insuffisant ");
            
        
    }
    

    [HttpDelete("{numCompte}")]
    public async Task<ActionResult<string>> DeleteCompte(string numCompte)
    {
        var result = await _compteService.DeleteCompte(numCompte);
        if (!result)
            return NotFound("Compte introuvable !");
        return $"Le compte {numCompte}  bien supprimé";
    }


    [HttpPost("GetClientSolde")]
    public async Task<ActionResult<string>> GetClientSolde(ClientSoldeDto clientSoldeDto)
    {
        var lesolde = await _compteService.GetClientSolde(clientSoldeDto.Tel);
        if (lesolde != null)
        {
            return Ok(lesolde);
        }

        return NotFound();
    }
    
    

}