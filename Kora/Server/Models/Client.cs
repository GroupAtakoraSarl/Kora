using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Models;

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
    
    
}