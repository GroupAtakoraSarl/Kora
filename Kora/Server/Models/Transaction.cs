using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Transaction
{
    [Key]
    public int IdTransaction { get; set; }
    public DateTime DateTransaction { get; set; }
    public int NumExp { get; set; }
    public decimal Montant { get; set; }
    public string Type { get; set; }
    
    [ForeignKey("IdCompte")]
    public Compte Compte { get; set; }
}