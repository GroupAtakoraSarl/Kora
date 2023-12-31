using System.ComponentModel.DataAnnotations;

namespace Kora.Shared.Models;

public class Client
{
    [Key] public int IdClient { get; set; }

    [Required] public string Username { get; set; }

    [Required] public string Tel { get; set; }

    [Required] public string Password { get; set; }

    public ICollection<Compte> Comptes { get; set; }
}