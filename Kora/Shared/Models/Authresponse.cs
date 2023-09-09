namespace Kora.Shared.Models;

public class AuthResponse
{
    public string Errors { get; set; }
    public string Username { get; set; }

    public string Email { get; set; }

    public string Tel { get; set; }
    public decimal Solde { get; set; }
}