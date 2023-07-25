using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Compte
{
    [Key]
    public int IdCompte { get; set; }
    public int NumCompte { get; set; }
    public decimal Solde { get; set; }
    
    [ForeignKey("IdClient")]
    public Client Client { get; set; }
    
    public List<Transaction> Transactions { get; set; }
}