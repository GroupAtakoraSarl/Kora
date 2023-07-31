using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IPaysService
{
    Task<List<PaysDto>> GetAllPays();
    Task<PaysDto> GetPaysByIndi(int indicatif);
    Task<Pays> AddPays(Pays pays);
    Task<bool> DeletePays(int indicatif);
}