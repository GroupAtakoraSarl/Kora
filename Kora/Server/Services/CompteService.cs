using AutoMapper;
using Kora.Models;
using Kora.Server.Data;

namespace Kora.Server.Services;

public class CompteService : ICompteService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public CompteService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<Compte> AddCompte(Compte compte)
    {
        var lecompte = _mapper.Map<Compte>(compte);
        _dbContext.Comptes.Add(compte);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Compte>(lecompte);
    }

    public async Task<bool> DepotCompte(string numCompte, decimal solde)
    {
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == numCompte);
        if (compte is null)
        {
            return false;
        }
        
        compte.Solde += solde;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RetraitCompte(string numCompte, decimal solde)
    {
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == numCompte);
        if (compte is null)
        {
            return false;
        }

        if (solde >= compte.Solde)
        {
            return false;
        }
        
        compte.Solde -= solde;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Transfert(string numCompte, decimal solde)
    {
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte != numCompte);
        if (compte is null)
        {
            return false;
        }

        compte.Solde += solde;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCompte(string numCompte)
    {
        var compte = await _dbContext.Comptes.FindAsync(numCompte);
        if (compte is null)
        {
            return false;
        }

        _dbContext.Comptes.Remove(compte);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }
}