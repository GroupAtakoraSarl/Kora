using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class KiosqueService : IKiosqueService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public KiosqueService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<List<Kiosque>> GetAllKiosque()
    {
        var kiosques = await _dbContext.Kiosques.ToListAsync();
        return kiosques;
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
        var lekiosque = _dbContext.Kiosques.Add(kiosque);
        await _dbContext.SaveChangesAsync();
        return lekiosque.Entity;
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