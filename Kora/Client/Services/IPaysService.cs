using System.Text.Json;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IPaysService
{
    Task<List<Pays>> GetAllPays();
    Task<Pays> GetPaysNameById(int idPays);
    Task<Pays> AddPays(Pays pays);
    Task<bool> DeletePays(int indicatif);
}