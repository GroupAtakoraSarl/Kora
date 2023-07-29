using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Ville
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? IdVille { get; set; }
    public string NomVille { get; set; }
    
    public int IdPays { get; set; }
    public Pays Pays { get; set; }
    
}