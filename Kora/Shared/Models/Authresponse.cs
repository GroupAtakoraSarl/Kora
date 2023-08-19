namespace Kora.Shared.Models;

public class AuthResponse
{
    public string Errors { get; set; }
    public string Username { get; set; }
    public string Email {
        get;
        set;
    }
}