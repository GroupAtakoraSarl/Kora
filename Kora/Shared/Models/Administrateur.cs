using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Kora.Shared.Models;

public class Administrateur
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAdmin { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    
}