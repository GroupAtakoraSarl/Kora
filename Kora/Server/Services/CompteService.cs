using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class CompteService : ICompteService
{
    private readonly KoraDbContext _dbContext;
    
    private List<string> transactions = new List<string>();
    private decimal _devise = 500m;

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

    
    public async Task<bool> Transfert(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
    {
        try
        {
            var expediteur = _dbContext.Comptes
                .Include(c => c.Client)
                .FirstOrDefault(c => c.NumCompte == numCompteExpediteur);
            var destinataire = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == numCompteDestinataire);

            if (expediteur is null || destinataire is null)
            {
                return false;
            }

            if (expediteur.Solde < solde)
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
            var fraisCFA = await Conversion2Kora(frais);
            var soldeFCFA = await Conversion2Kora(solde);

            var depotTransaction = new Transaction
            {
                Date = DateTime.Now,
                NumExp = expediteur.NumCompte,
                NumDes = destinataire.NumCompte,
                Frais = fraisCFA,
                Type = Transaction.TransactionType.Transfert,
                Solde = soldeFCFA
            };

            expediteur.Transactions.Add(depotTransaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Retrait(string numCompte, decimal solde, string code, string password)
    {
        try
        {
            var compte = await _dbContext.Comptes.FirstOrDefaultAsync(c => c.NumCompte == numCompte);
            var client = _dbContext.Clients.FirstOrDefault(c => c.Tel == numCompte);
            var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(k => k.Code == code);

            if (compte == null || kiosque == null)
            {
                throw new Exception();
            }


            if (compte.Client == null)
            {
                throw new Exception();
            }

            bool isPwdCorrect = BCrypt.Net.BCrypt.Verify(password, client.Password);
            if (!isPwdCorrect)
            {
                throw new Exception();
            }

            if (solde > compte.Solde)
            {
                throw new Exception();
            }
            
            compte.Solde -= solde;

            var frais = solde * 0.05m;
            var fraisCfa = await Conversion2Kora(frais);
            var soldeCFA = await Conversion2Kora(solde);
            
            var retraitTransaction = new Transaction
            {
                Date = DateTime.Now,
                NumExp = compte.NumCompte,
                NumDes = kiosque.Code,
                Frais = fraisCfa,
                Type = Transaction.TransactionType.Retrait,
                Solde = soldeCFA,
                IdCompte = compte.IdCompte
            };

            compte.Transactions.Add(retraitTransaction);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task Depot(string numCompte, string code, decimal solde)
    {
        try
        {
            var compte = await _dbContext.Comptes
                .FirstOrDefaultAsync(c => c.NumCompte == numCompte);
            var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(k => k.Code == code);
            if (compte == null || kiosque == null)
            {
                throw new Exception();
            }


            if (kiosque.Solde < solde)
            {
                throw new Exception();
            }

            var newSolde = await ConversionKora(solde);
            var lesolde = newSolde;


            compte.Solde += lesolde;
            kiosque.Solde -= solde;


            var transfertTransaction = new Transaction
            {
                Date = DateTime.Now,
                NumDes = compte.NumCompte,
                NumExp = kiosque.Code,
                Frais = 0,
                Type = Transaction.TransactionType.Rechargement,
                Solde = solde
            };

            compte.Transactions.Add(transfertTransaction);

            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    public async Task<decimal> ConversionKora(decimal solde)
    {
        var somme = solde / Devise;
        return somme;
    }
    

    public async Task<decimal> Conversion2Kora(decimal solde)
    {
        var somme = solde * Devise;
        return somme;
    }

    public async Task<decimal?> GetClientSolde(string tel)
    {
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Tel == tel);
        if (leclient != null)
        {
            var lecompte = _dbContext.Comptes.FirstOrDefault(c => c.IdClient == leclient.IdClient);
            if (leclient != null)
            {
                return lecompte.Solde;
            }
        }

        return null;
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