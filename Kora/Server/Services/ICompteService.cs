using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface ICompteService
{
    Task<List<Compte>> GetAllComptes();
    Task<CompteDto> GetCompteByNum(string numCompte);
    Task<bool> Transfert(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde);
    Task Retrait(string numCompte,  decimal solde, string code, string password);
    Task Depot(string numCompte, string code, decimal solde);
    Task<decimal> ConversionKora(decimal solde);
    Task<decimal> Conversion2Kora(decimal solde);
    Task<decimal?> GetClientSolde(string tel);

    Task<List<CompteDto>> GetCompteByClientId(int idClient);
    Task<bool> DeleteCompte(string numCompte);

}