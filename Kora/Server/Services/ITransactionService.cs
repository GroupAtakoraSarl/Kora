using Kora.Shared.Models;

namespace Kora.Server.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllTransaction();
}