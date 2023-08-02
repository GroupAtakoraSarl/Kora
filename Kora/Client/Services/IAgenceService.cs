using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IAgenceService
{
    Task<List<Agence>> GetAllAgence();
    Task<AgenceDto> GetAgenceByContact(string contactAgence);
    Task<Agence> AddAgence(Agence agence);
    Task<bool> DeleteAgence(string contactAgence);
    
}