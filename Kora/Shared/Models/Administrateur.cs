using System.ComponentModel.DataAnnotations;

namespace Kora.Shared.Models;

public class Administrateur
{
    [Key] public int IdAdmin { get; set; }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}