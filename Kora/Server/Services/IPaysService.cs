using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IPaysService
{
    Task<Pays> AddPays(Pays pays);
    Task<bool> DeletePays(int indicatif);
}