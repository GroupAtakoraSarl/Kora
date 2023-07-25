using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

public class Ville
{
    [Key] 
    public int? IdVille { get; set; }
    public string NomVille { get; set; }
    
    [ForeignKey(("IdPays"))]
    public Pays Pays { get; set; }
    
}