using Kora.Shared.Models;

namespace Kora.Shared.ModelsDto;

public class TransactionDto
{
    public int IdTransaction { get; set; }
    public decimal Solde { get; set; }
    public string NumExp { get; set; }
    public string? NumDes { get; set; }
    public decimal Frais { get; set; }
    public DateTime Date { get; set; }
    public  Transaction.TransactionType Type { get; set; }

    public int IdCompte { get; set; }
}