using Kora.Server.Data;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class TransactionService : ITransactionService
{
    private readonly KoraDbContext _dbContext;
    private readonly ICompteService _compteService;
    private readonly IClientService _clientService;

    public TransactionService(KoraDbContext dbContext, ICompteService compteService, IClientService clientService)
    {
        _clientService = clientService;
        _compteService = compteService;
        _dbContext = dbContext;
    }
    
    public async Task<List<Transaction>> GetAllTransaction()
    {
        var transactions = await _dbContext.Transactions.ToListAsync();
        return transactions;
    }

    public async Task<List<Transaction>> GetClientTransaction(string tel)
    {
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Tel == tel);
        if (leclient != null)
        {
            var lecompte = _dbContext.Comptes.FirstOrDefault(cp => cp.NumCompte == leclient.Tel);

            if (lecompte != null)
            {
                var transaction = await _dbContext.Transactions
                    .Where(t => t.NumDes == lecompte.NumCompte || t.NumExp == lecompte.NumCompte)
                    .ToListAsync();

                return transaction;
            }
        }

        return new List<Transaction>();
    }

    public async Task<List<Transaction>> GetKiosqueTransaction(string code)
    {
        var lekiosque = _dbContext.Kiosques.FirstOrDefault(k => k.Code == code);
        if (lekiosque != null)
        {
            var transaction = await _dbContext.Transactions
                .Where(t => t.NumDes == code || t.NumExp == code)
                .ToListAsync();
            return transaction;
        }

        return new List<Transaction>();
    }
}