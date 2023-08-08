using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IPaysService
{
    Task<List<Pays>> GetAllPays();
    Task<Pays> AddPays(Pays pays);
    Task<bool> DeletePays(int indicatif);
}