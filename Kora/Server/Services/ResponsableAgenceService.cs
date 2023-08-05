using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
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
    
    public async Task<List<ResponsableAgence>> GetAllResponsable()
    {
        var responsables = await _dbContext.ResponsableAgences.ToListAsync();
        return _mapper.Map<List<ResponsableAgence>>(responsables);
    }

    public async Task<ResponsableAgenceDto> GetResponsableByTel(string tel)
    {
        var responsable = await _dbContext.ResponsableAgences.FindAsync(tel);
        return _mapper.Map<ResponsableAgenceDto>(responsable);
    }

    public async Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence)
    {
        var newRes = _mapper.Map<ResponsableAgence>(responsableAgence);
        _dbContext.ResponsableAgences.Add(newRes);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<ResponsableAgence>(responsableAgence);
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