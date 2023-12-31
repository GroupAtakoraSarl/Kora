using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Kiosque
{
    [Key] public int IdKiosque { get; set; }

    public string NomKiosque { get; set; }
    public string Code { get; set; }
    public string Key { get; set; }
    public decimal Solde { get; set; }
    public string Password { get; set; }
    public string AdresseKiosque { get; set; }
    public string ContactKiosque { get; set; }

    [ForeignKey("Agence")] public int IdAgence { get; set; }

    public Agence Agence { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

}