using Kora.Shared.Models;

namespace Kora.Server.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllTransaction();
    Task<List<Transaction>> GetClientTransaction(int idClient);
}