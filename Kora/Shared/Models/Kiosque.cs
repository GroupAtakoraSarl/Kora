using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Kiosque
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int IdKiosque { get; set; }
    public string NomKiosque { get; set; }
    public string AdresseKiosque { get; set; }
    public string ContactKiosque { get; set; }

    [ForeignKey("Agence")]
    public int IdAgence { get; set; }
    public Agence Agence { get; set; }
    
}