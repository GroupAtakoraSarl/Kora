using AutoMapper;
using Kora.Server.Data;
using Kora.Server.ModelsDto;
using Kora.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class ResponsableAgenceService : IResponsableAgence
{

    private readonly KoraDbContext _dbContext;
    private readonly IMapper _mapper;

    public ResponsableAgenceService(KoraDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<ResponsableAgenceDto>> GetAllResponsable()
    {
        var responsables = await _dbContext.ResponsableAgences.ToListAsync();
        return _mapper.Map<List<ResponsableAgenceDto>>(responsables);
    }

    public async Task<bool> DeleteResponsable(int tel)
    {
        var responsable = await _dbContext.ResponsableAgences.FindAsync(tel);
        if (responsable is null)
            return false;

        _dbContext.ResponsableAgences.Remove(responsable);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}