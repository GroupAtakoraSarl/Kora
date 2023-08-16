using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Kora.Server.Services;

public class CompteService : ICompteService
{
    private readonly KoraDbContext _dbContext;
    
    private List<string> transactions = new List<string>();
    private decimal _devise = 2.5m;

    public CompteService(IKiosqueService kiosqueService, KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public decimal Devise
    {
        get => _devise;
        set
        {
            if (_devise != value)
            {
                _devise = value;
            }
        }
    }
    
    public async Task<List<Compte>> GetAllComptes()
    {
        var comptes = await _dbContext.Comptes.ToListAsync();
        return comptes;
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

    public async Task<bool> Transfert(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde, decimal frais)
    {
        var expediteur = _dbContext.Comptes
            .Include(c => c.Client)
            .FirstOrDefault(c=>c.NumCompte == numCompteExpediteur);
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
        frais = solde * 0.05m;
        expediteur.Solde -= frais;

        var depotTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumExp = expediteur.NumCompte,
            NumDes = destinataire.NumCompte,
            Frais = frais,
            Type = Transaction.TransactionType.Dépôt,
            Solde = solde
        };
        
        expediteur.Transactions.Add(depotTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Retrait(string numCompte, decimal solde, string code, string password)
    {
        var compte = await _dbContext.Comptes
            .Include(compte => compte.Client)
            .FirstOrDefaultAsync(c => c.NumCompte == numCompte);
        var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(k => k.Code == code);
        if (compte == null || kiosque == null)
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

        var Notification = new Notification
        {
            Solde = solde,
            NomClient = compte.Client.Username,
            Type = Shared.Models.Notification.NotifType.Dépôt
        };
        
        var retraitTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumExp = compte.NumCompte,
            NumDes = "none",
            Type = Transaction.TransactionType.Retrait,
            Solde = solde
        };
        
        compte.Transactions.Add(retraitTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<bool> Depot(string numCompte, string code, decimal solde, decimal frais)
    {
        var compte = await _dbContext.Comptes
            .FirstOrDefaultAsync(c => c.NumCompte == numCompte);
        var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(k => k.Code == code);
        if (compte == null || kiosque == null)
        {
            return false;
        }

        compte.Solde += solde;
        kiosque.Solde -= solde;
        frais = solde * 0.5m;
        compte.Solde -= frais;
        compte.Solde = await ConversionKora(compte.Solde);

        var Notification = new Notification
        {
            Solde = solde,
            NomClient = compte.Client.Username,
            Type = Shared.Models.Notification.NotifType.Dépôt
        };
        
        var transfertTransaction = new Transaction
        {
            Date = DateTime.Now,
            NumDes = compte.NumCompte,
            Frais = frais,
            Type = Transaction.TransactionType.Transfert,
            Solde = solde
        };
        
        compte.Transactions.Add(transfertTransaction);
        
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<decimal> ConversionKora(decimal solde)
    {
        var somme = solde / Devise;
        return somme;
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