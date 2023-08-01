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
    
    public async Task<List<CompteDto>> GetAllComptes()
    {
        var comptes = await _dbContext.Comptes.ToListAsync();
        return _mapper.Map<List<CompteDto>>(comptes);
    }
    
    public async Task<Compte> AddCompte(Compte compte, int idClient)
    {
        var existingClient = await _dbContext.Clients.FindAsync(idClient);
        if (existingClient is null)
        {
            return null;
        }

        compte.Client = existingClient;
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
        
        await _dbContext.SaveChangesAsync();
        
        transactions.Add($"Dépôt depuis le Compte : {numCompteExpediteur} vers le Compte : {numCompteDestinataire}, Montant : {solde}, Frais : {frais} Date : " + DateTime.Now);

        return true;
    }

    public async Task<bool> RetraitCompte(string numCompte, decimal solde, string password)
    {
        var compte = _dbContext.Comptes.Include(c => c.Client).FirstOrDefault(c => c.NumCompte == numCompte);
        if (compte is null)
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
        transactions.Add($"Retrait sur le Compte : {numCompte}, Montant : {solde}. Date : "+DateTime.Now);
        await _dbContext.SaveChangesAsync();

        return true;
    }


    public async Task<bool> Transfert(string numCompte, decimal solde)
    {
        var compte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == numCompte);
        if (compte is null)
        {
            return false;
        }

        compte.Solde += solde;
        transactions.Add($"Transafert vers le Compte : {numCompte}, Montant : {solde}. Date : "+DateTime.Now);
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

    public async Task<List<string>> GetTransaction()
    {
        return transactions;
    }
    
}