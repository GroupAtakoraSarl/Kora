using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Compte
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCompte { get; set; }
    public string NumCompte { get; set; }
    public decimal Solde { get; set; }

    public int IdClient { get; set; }
    public Client Client { get; set; }
    
}