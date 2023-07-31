using AutoMapper;
using Kora.Models;
using Kora.Server.Data;
using Kora.Server.ModelsDto;
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
    
    public async Task<List<KiosqueDto>> GetAllKiosque()
    {
        var kiosques = await _dbContext.Kiosques.ToListAsync();
        return _mapper.Map<List<KiosqueDto>>(kiosques);
    }

    public async Task<List<KiosqueDto>> GetKiosqueByAdresse(string adresseKiosque)
    {
        var kiosque = await _dbContext.Kiosques
            .Where(k => k.AdresseKiosque == adresseKiosque)
            .ToListAsync();
        
        return _mapper.Map<List<KiosqueDto>>(kiosque);
    }
    
    public async Task<Kiosque> AddKiosque(Kiosque kiosque)
    {
        var lekiosque = _mapper.Map<Kiosque>(kiosque);
        _dbContext.Kiosques.Add(lekiosque);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Kiosque>(kiosque);
    }

    public async Task<bool> DeleteKiosque(int contactKiosque)
    {
        var kiosque = await _dbContext.Kiosques.FindAsync(contactKiosque);
        if (kiosque is null)
        {
            return false;
        }

        _dbContext.Kiosques.Remove(kiosque);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}