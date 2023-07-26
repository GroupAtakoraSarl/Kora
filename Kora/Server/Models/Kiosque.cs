using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Kiosque
{
    [Key]
    public int? IDKiosque { get; set; }
    public string NomKiosque { get; set; }
    public string AdresseKiosque { get; set; }
    public string ContactKiosque { get; set; }
    
    [ForeignKey("IdAgence")]
    public Agence Agence { get; set; }
    
}