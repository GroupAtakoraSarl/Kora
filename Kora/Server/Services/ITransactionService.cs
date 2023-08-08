using System.Transactions;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllTransaction();
}