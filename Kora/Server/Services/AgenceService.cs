using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class AgenceService : IAgenceService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public AgenceService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<List<AgenceDto>> GetAllAgence()
    {
        var agences = await _dbContext.Agences.ToListAsync();
        return _mapper.Map<List<AgenceDto>>(agences);
    }
    
    public async Task<AgenceDto> GetAgenceByContact(string contactAgence)
    {
        var agence = await _dbContext.Agences.FindAsync(contactAgence);
        return _mapper.Map<AgenceDto>(agence);
    }
    
    public async Task<Agence> AddAgence(Agence agence)
    {
        var lagence = _mapper.Map<Agence>(agence);
        _dbContext.Agences.Add(lagence);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Agence>(agence);
    }
    
    public async Task<bool> DeleteAgence(string contactAgence)
    {
        var agence = await _dbContext.Agences.FindAsync(contactAgence);
        if (agence is null)
        {
            return false;
        }

        _dbContext.Agences.Remove(agence);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}