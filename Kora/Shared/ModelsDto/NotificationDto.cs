namespace Kora.Shared.ModelsDto;

public class NotificationDto
{
    public string NomClient { get; set; }
    public decimal Solde { get; set; }
    public enum NotifType
    {
        Dépôt,
        Retrait,
        Transfert
    }

    public NotifType Type { get; set; }
}