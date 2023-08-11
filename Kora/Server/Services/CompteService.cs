using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class CompteService : ICompteService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;
    
    private List<string> transactions = new List<string>();

    public CompteService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<List<Compte>> GetAllComptes()
    {
        var comptes = await _dbContext.Comptes.ToListAsync();
        return comptes;
    }
    
    public async Task<Compte> AddCompte(Compte compte)
    {
        _dbContext.Comptes.Add(compte);
        await _dbContext.SaveChangesAsync();
        return compte;
    }

    
    public async Task<CompteDto> GetCompteByNum(string numCompte)
    {
        var compte = await _dbContext.Comptes
            .Include(c => c.Client)
            .FirstOrDefaultAsync(c => c.NumCompte == numCompte);

        if (compte is null)
        {
            return null;
        }

        var compteDto = new CompteDto
        {
            NumCompte = compte.NumCompte,
            Solde = compte.Solde,
            IdClient = compte.Client?.IdClient ?? 0
        };
        return compteDto;
    }

    public async Task<bool> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
    {
        var expediteur = _dbContext.Comptes.Include(c => c.Client).FirstOrDefault(c=>c.NumCompte == numCompteExpediteur);
        var destinataire = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == numCompteDestinataire);

        if (expediteur is null || destinataire is null)
        {
            return false;
        }
        
        var isPwdCorrect = BCrypt.Net.BCrypt.Verify(passwordExpediteur, expediteur.Client.Password);

        if (!isPwdCorrect)
        {
            return false;
        }

        expediteur.Solde -= solde;
        destinataire.Solde += solde;
        var frais = solde * 0.05m;
        expediteur.Solde -= frais;

        var depotTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumExp = expediteur.NumCompte,
            NumDes = destinataire.NumCompte,
            Type = Transaction.TransactionType.Dépôt,
            Solde = solde
        };
        
        expediteur.Transactions.Add(depotTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RetraitCompte(string numCompte, decimal solde, string password)
    {
        var compte = await _dbContext.Comptes
            .Include(compte => compte.Client)
            .FirstOrDefaultAsync(c => c.NumCompte == numCompte);
        if (compte == null)
        {
            return false;
        }

        if (compte.Client == null)
        {
            return false;
        }

        bool isPwdCorrect = BCrypt.Net.BCrypt.Verify(password, compte.Client.Password);
        if (!isPwdCorrect)
        {
            return false;
        }

        if (solde >= compte.Solde)
        {
            return false;
        }
        
        compte.Solde -= solde;

        var retraitTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumExp = compte.NumCompte,
            Type = Transaction.TransactionType.Retrait,
            Solde = solde
        };
        
        compte.Transactions.Add(retraitTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }


    public async Task<bool> Transfert(string numCompte, decimal solde)
    {
        var compte = await _dbContext.Comptes.FirstOrDefaultAsync(c => c.NumCompte == numCompte);
        if (compte == null)
        {
            return false;
        }

        compte.Solde += solde;

        var transfertTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumDes = compte.NumCompte,
            Type = Transaction.TransactionType.Transfert,
            Solde = solde
        };
        
        compte.Transactions.Add(transfertTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<CompteDto>> GetCompteByClientId(int idClient)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteCompte(string numCompte)
    {
        var compte = _dbContext.Comptes.FirstOrDefault(c=>c.NumCompte == numCompte);
        if (compte is null)
        {
            return false;
        }

        _dbContext.Comptes.Remove(compte);
        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    
}