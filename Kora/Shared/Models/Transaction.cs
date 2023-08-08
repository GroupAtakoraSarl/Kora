using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTransaction { get; set; }
    public decimal Solde { get; set; }
    public DateTime Date { get; set; }
    public enum TransactionType
    {
        Dépôt,
        Retrait,
        Transfert
    }
    public TransactionType Type { get; set; }

    [ForeignKey("Compte")]
    public int IdCompte { get; set; }
    public Compte Compte { get; set; }
    
}

