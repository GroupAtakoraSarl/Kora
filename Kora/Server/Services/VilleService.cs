using AutoMapper;
using Kora.Models;
using Kora.Server.Data;
using Kora.Server.Services;

namespace Kora_Transfert.Shared.Services;

public class VilleService : IVilleService
{
    private readonly KoraDbContext _dbContext;
    private readonly IMapper _mapper;

    public VilleService(KoraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<Ville> AddVille(Ville ville)
    {
        var laville = _mapper.Map<Ville>(ville);
        _dbContext.Villes.Add(laville);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Ville>(ville);
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