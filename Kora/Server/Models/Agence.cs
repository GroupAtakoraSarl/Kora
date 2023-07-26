using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Agence
{

    [Key] 
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
    
    [ForeignKey("IdResponsable")]
    public ResponsableAgence ResponsableAgence { get; set; }
    

}