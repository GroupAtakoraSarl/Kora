using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface ITransactionService
{
    Task<List<Transaction>> GetAllTransaction();
    Task<List<Transaction>> GetClientTransaction(string tel);
    Task<List<Transaction>> GetKiosqueTransaction(string code);
}