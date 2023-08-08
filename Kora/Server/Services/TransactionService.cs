using Kora.Server.Data;
using Kora.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class TransactionService : ITransactionService
{
    private readonly KoraDbContext _dbContext;

    public TransactionService(KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Transaction>> GetAllTransaction()
    {
        var transactions = await _dbContext.Transactions.ToListAsync();
        return transactions;
    }
}