using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface ICompteService
{
    Task<List<Compte>> GetAllComptes();
    Task<CompteDto> GetCompteByNum(string numCompte);
    // Task<bool> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde);
    // Task<bool> RetraitCompte(string numCompte, decimal solde, string password);
    // Task<bool> Transfert(string numCompte, decimal solde);
    Task<List<CompteDto>> GetCompteByClientId(int clientId);
    Task<bool> DeleteCompte(string numCompte);
    Task<List<string>> GetTransaction();

}