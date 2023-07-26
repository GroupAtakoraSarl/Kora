
using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface ITransactionService
{
    Task<TransactionDto> GetInfoTransac(DateTime date, decimal montant, string numExp, string type);


    // public DateTime DateTransaction { get; set; }
    // public decimal Montant { get; set; }
    // public int NumExp { get; set; }
    // public string Type { get; set; }

}