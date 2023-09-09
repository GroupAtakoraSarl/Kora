using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Ville
{
    [Key] public int IdVille { get; set; }

    public string NomVille { get; set; }

    [ForeignKey("Pays")] public int IdPays { get; set; }

    public Pays Pays { get; set; }
}