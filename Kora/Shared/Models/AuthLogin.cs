namespace Kora.Shared.Models;

public class AuthLogin
{
    public string Tel { get; set; }
    public string Username { get; set; }
    public decimal Solde { get; set; }
    public IEnumerable<Transaction> Transactions { get; set; }
    public string Error { get; set; }
}