using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IAgenceService
{
    Task<List<Agence>> GetAllAgence();
    Task<Agence> GetAgenceByContact(string contactAgence);
    Task<Agence> AddAgence(Agence agence);
    Task<bool> DeleteAgence(string contactAgence);
    
}