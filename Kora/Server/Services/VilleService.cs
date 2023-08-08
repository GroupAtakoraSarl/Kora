using Kora.Shared.Models;
using Kora.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class VilleService : IVilleService
{
    private readonly KoraDbContext _dbContext;

    public VilleService(KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Ville>> GetAllVille()
    {
        var villes = await _dbContext.Villes.ToListAsync();
        return villes;
    }
    
    public async Task<Ville> AddVille(Ville ville)
    {
        var laville = _dbContext.Villes.Add(ville);
        
        await _dbContext.SaveChangesAsync();
        return laville.Entity;
    }

    public async Task<bool> DeleteVille(string nomVille)
    {
        var ville = await _dbContext.Villes.FindAsync(nomVille);
        if (ville is null)
            return false;

        _dbContext.Villes.Remove(ville);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}