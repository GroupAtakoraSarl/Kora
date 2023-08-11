using Kora.Shared.Models;

namespace Kora.Client.Services;

public interface IVilleService
{
    Task<List<Ville>> GetAllVille();
    Task<List<Ville>> GetAllVilleWithPays();
    Task<Ville> AddVille(Ville ville);
    Task<bool> DeleteVille(string nomVille);
}