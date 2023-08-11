using System.Text.Json;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IVilleService
{
    Task<List<Ville>> GetAllVille();
    Task<List<Ville>> GetAllVilleWithPays();
    Task<Ville> AddVille(Ville ville);
    Task<bool> DeleteVille(string nomVille);
}
