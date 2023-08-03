using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IPaysService
{
    Task<List<PaysDto>> GetAllPays();
    Task<Pays> AddPays(Pays pays);
    Task<bool> DeletePays(int indicatif);
}