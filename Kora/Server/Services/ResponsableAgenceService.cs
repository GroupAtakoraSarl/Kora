using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class ResponsableAgenceService : IResponsableAgence
{

    private readonly KoraDbContext _dbContext;

    public ResponsableAgenceService(KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<ResponsableAgence>> GetAllResponsable()
    {
        var responsables = await _dbContext.ResponsableAgences.ToListAsync();
        return responsables;
    }

    public async Task<ResponsableAgence> GetResponsableByTel(string tel)
    {
        var responsable = await _dbContext.ResponsableAgences.FindAsync(tel);
        return responsable;
    }

    public async Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence)
    {
        var newRes = _dbContext.ResponsableAgences.Add(responsableAgence);
        await _dbContext.SaveChangesAsync();
        return newRes.Entity;
    }
    
    public async Task<bool> DeleteResponsable(string tel)
    {
        var responsable = await _dbContext.ResponsableAgences.FindAsync(tel);
        if (responsable is null)
            return false;

        _dbContext.ResponsableAgences.Remove(responsable);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}