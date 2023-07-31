using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IAgenceService
{
    Task<List<AgenceDto>> GetAllAgence();
    Task<AgenceDto> GetAgenceByContact(string contactAgence);
    Task<Agence> AddAgence(Agence agence);
    Task<bool> DeleteAgence(string contactAgence);
    
}