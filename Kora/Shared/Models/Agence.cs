using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Agence
{

    [Key] 
    public int IdAgence { get; set; }
    public string Pays { get; set; }
    public string Ville { get; set; }
    public string NomAgence { get; set; }
    public string AdresseAgence { get; set; }
    public string ContactAgence { get; set; }
    public string EmailAgence { get; set; }
    public string DeviseAgence { get; set; }

    [ForeignKey("ResponsableAgence")]
    public int IdResponsable { get; set; }
    public ResponsableAgence ResponsableAgence { get; set; }
    
    public ICollection<Kiosque> Kiosques { get; set; }
    
}