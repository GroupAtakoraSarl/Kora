using System.ComponentModel.DataAnnotations;

namespace Kora.Models;

public class Pays
{
    [Key]
    public int? IdPays { get; set; }
    [Required]
    public string NomPays { get; set; }
    [Required]
    public int Indicatif { get; set; }
    [Required]
    public string CodeISO { get; set; }
    
    [Required]
    public string DevisePays { get; set; }


}