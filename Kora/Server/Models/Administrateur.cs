using System.ComponentModel.DataAnnotations;

namespace Kora.Models;

public class Administrateur
{

    [Key]
    public int IdAdmin { get; set; }

    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    
}