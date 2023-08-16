using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class KiosqueService : IKiosqueService
{
    private readonly KoraDbContext _dbContext;

    public KiosqueService(IMapper mapper, KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Kiosque>> GetAllKiosque()
    {
        var kiosques = await _dbContext.Kiosques.ToListAsync();
        return kiosques;
    }
    
    public async Task<string> GenerateRandomCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        
        while (await _dbContext.Kiosques.AnyAsync(k => k.Code == code))
        {
            code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        return code;
    }
    
    
    public async Task<List<Kiosque>> GetKiosqueByAdresse(string adresseKiosque)
    {
        var kiosque = await _dbContext.Kiosques
            .Where(k => k.AdresseKiosque == adresseKiosque)
            .ToListAsync();

        return kiosque;
    }
    
    public async Task<Kiosque> AddKiosque(Kiosque kiosque)
    {
        kiosque.Code = await GenerateRandomCode();
        while (await _dbContext.Kiosques.AnyAsync(k => k.Code == kiosque.Code))
        {
            kiosque.Code = await GenerateRandomCode();
        }

        _dbContext.Kiosques.Add(kiosque);
        await _dbContext.SaveChangesAsync();
        return kiosque;
    }
    
    public async Task<bool> ChargeSolde(decimal solde, string contactKiosque)
    {
        var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(a => a.ContactKiosque == contactKiosque);
        if (kiosque is not null)
        {
            kiosque.Solde += solde;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }

    }
    
    public async Task<bool> DeleteKiosque(string contactKiosque)
    {
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k=>k.ContactKiosque == contactKiosque);
        if (kiosque is null)
        {
            return false;
        }

        _dbContext.Kiosques.Remove(kiosque);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}