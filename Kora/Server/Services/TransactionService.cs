using System.Transactions;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class TransactionService : ITransactionService
{

    private readonly KoraDbContext _dbContext;
    
    
    public async Task<List<Transaction>> GetAllTransaction()
    {
        var transactions = await _dbContext.Transactions.ToListAsync();
        return transactions;

    }
}