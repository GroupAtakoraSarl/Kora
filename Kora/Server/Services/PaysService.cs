using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class PaysService : IPaysService
{
    private readonly KoraDbContext _dbContext;
    private readonly IMapper _mapper;

    public PaysService(KoraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<List<PaysDto>> GetAllPays()
    {
        var pays = await _dbContext.Pays.ToListAsync();
        return _mapper.Map<List<PaysDto>>(pays);
    }

    public async Task<Pays> AddPays(Pays pays)
    {
        var lepays = _mapper.Map<Pays>(pays);
        _dbContext.Pays.Add(lepays);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Pays>(pays);
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