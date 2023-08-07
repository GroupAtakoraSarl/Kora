using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Agence
{

    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int IdAgence { get; set; }
    [Required] 
    public string Pays { get; set; }
    [Required] 
    public string Ville { get; set; }
    [Required] 
    public string NomAgence { get; set; }
    [Required] 
    public string AdresseAgence { get; set; }
    [Required] 
    public string ContactAgence { get; set; }
    [Required] 
    public string EmailAgence { get; set; }
    [Required] 
    public string DeviseAgence { get; set; }
    [Required] 
    public double SoldeInitial { get; set; }

    public int IdResponsable { get; set; }
    public ResponsableAgence ResponsableAgence { get; set; }
    
    public ICollection<Agence> Agences { get; set; }
    
}