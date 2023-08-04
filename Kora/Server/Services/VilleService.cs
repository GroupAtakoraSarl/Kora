using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class VilleService : IVilleService
{
    private readonly KoraDbContext _dbContext;
    private readonly IMapper _mapper;

    public VilleService(KoraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<VilleDto>> GetAllVille()
    {
        var villes = await _dbContext.Villes.ToListAsync();
        return _mapper.Map<List<VilleDto>>(villes);
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