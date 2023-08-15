using Kora.Shared.Models;
using Kora.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class AgenceService : IAgenceService
{
    private readonly KoraDbContext _dbContext;

    public AgenceService(KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Agence>> GetAllAgence()
    {
        var agences = await _dbContext.Agences.ToListAsync();
        return agences;
    }
    
    public async Task<Agence> GetAgenceByContact(string contactAgence)
    {
        var agence = await _dbContext.Agences.FindAsync(contactAgence);
        return agence;
    }
    
    public async Task<Agence> AddAgence(Agence agence)
    {
        _dbContext.Agences.Add(agence);
        await _dbContext.SaveChangesAsync();
        return agence;
    }

    


    public async Task<bool> DeleteAgence(string contactAgence)
    {
        var agence = _dbContext.Agences.FirstOrDefault(a=>a.ContactAgence == contactAgence);
        if (agence is null)
        {
            return false;
        }

        _dbContext.Agences.Remove(agence);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}