using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Kiosque
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IDKiosque { get; set; }
    public string NomKiosque { get; set; }
    public string AdresseKiosque { get; set; }
    public string ContactKiosque { get; set; }

    public int IdAgence { get; set; }
    public Agence Agence { get; set; }
    
}