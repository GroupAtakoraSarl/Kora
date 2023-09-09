using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Transaction
{
    public enum TransactionType
    {
        Rechargement,
        Retrait,
        Transfert
    }

    [Key] public int IdTransaction { get; set; }

    public decimal Solde { get; set; }
    public string NumExp { get; set; }
    public string NumDes { get; set; }
    public decimal Frais { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }

    [ForeignKey("Compte")] public int IdCompte { get; set; }

    public Compte Compte { get; set; }
}