using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class PaysService : IPaysService
{
    private readonly KoraDbContext _dbContext;

    public PaysService(KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<List<Pays>> GetAllPays()
    {
        var pays = await _dbContext.Pays.ToListAsync();
        return pays;
    }

    public async Task<Pays> AddPays(Pays pays)
    {
        var lepays = _dbContext.Pays.Add(pays);
        await _dbContext.SaveChangesAsync();
        return lepays.Entity;
    }

    public async Task<bool> DeletePays(int indicatif)
    {
        var pays = await _dbContext.Pays.FindAsync(indicatif);
        if (pays is null)
        {
            return false;
        }

        _dbContext.Pays.Remove(pays);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
}