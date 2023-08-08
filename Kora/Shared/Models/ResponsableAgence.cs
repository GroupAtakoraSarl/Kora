using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class ResponsableAgence
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int  IdResponsable { get; set; }
    [Required]
    public string NomResponsable { get; set; }
    [Required]

    public string PrenomResponsable { get; set; }
    [Required]

    public string SexeResponsable { get; set; }
    [Required]

    public int AgeResponsable { get; set; }
    [Required]

    public string Tel { get; set; }
    [Required]
    public string StatutResponsable { get; set; }
    

}