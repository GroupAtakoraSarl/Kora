using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Client
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdClient { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Tel { get; set; }
    [Required]
    public string Password { get; set; }

    public ICollection<Compte> Comptes { get; set; }
    
}