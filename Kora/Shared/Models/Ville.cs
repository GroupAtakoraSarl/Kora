using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Ville
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVille { get; set; }
    public string NomVille { get; set; }
    [ForeignKey("Pays")]
    public int IdPays { get; set; }
    public Pays Pays { get; set; }
}