namespace Kora.Server.ModelsDto;

public class TransactionDto
{
    public DateTime DateTransaction { get; set; }
    public decimal Montant { get; set; }
    public int NumExp { get; set; }
    public string Type { get; set; }

}