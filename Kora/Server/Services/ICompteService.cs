using Kora.Models;

namespace Kora.Server.Services;

public interface ICompteService
{
    Task<Compte> AddCompte(Compte compte);
    Task<bool> DepotCompte(int numCompte, decimal solde);
    Task<bool> RetraitCompte(int numCompte, decimal solde);
    Task<bool> DeleteCompte(int numCompte);
    
}