using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Compte
{
    [Key]
    public int IdCompte { get; set; }
    public string NumCompte { get; set; }
    public decimal Solde { get; set; }

    [ForeignKey("Client")]
    public int IdClient { get; set; }
    public Client Client { get; set; }

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();

}