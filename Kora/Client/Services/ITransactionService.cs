using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface ITransactionService
{
    Task<List<TransactionDto>> GetAllTransaction();

}