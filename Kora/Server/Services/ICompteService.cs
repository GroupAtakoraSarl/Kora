using Kora.Models;

namespace Kora.Server.Services;

public interface ICompteService
{
    Task<Compte> AddCompte(Compte compte);
    Task<bool> DepotCompte(string numCompte, decimal solde);
    Task<bool> RetraitCompte(string numCompte, decimal solde);
    Task<bool> Transfert(string numCompte, decimal solde);
    Task<bool> DeleteCompte(string numCompte);
    
}