using Kora.Shared.Models;

namespace Kora.Client.Services;

public interface IVilleService
{
    Task<List<Ville>> GetAllVille();

    Task<Ville> AddVille(Ville ville);
    Task<bool> DeleteVille(string nomVille);
}