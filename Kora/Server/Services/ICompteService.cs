using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface ICompteService
{
    Task<List<CompteDto>> GetAllComptes();
    Task<Compte> AddCompte(Compte compte, int idClient);
    Task<CompteDto> GetCompteByNum(string numCompte);
    Task<bool> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde);
    Task<bool> RetraitCompte(string numCompte, decimal solde);
    Task<bool> Transfert(string numCompte, decimal solde);
    Task<bool> DeleteCompte(string numCompte);
    Task<List<string>> GetTransaction();

}