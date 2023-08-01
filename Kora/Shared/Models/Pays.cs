using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Pays
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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