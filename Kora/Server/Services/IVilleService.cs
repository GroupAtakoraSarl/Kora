using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IVilleService
{
    Task<Ville> AddVille(Ville ville);
    Task<bool> DeleteVille(string nomVille);
}