using System.ComponentModel.DataAnnotations;

namespace Kora.Shared.Models;

public class NotificationClient
{
    public enum NotifType
    {
        Retrait,
        Transfert
    }

    [Key] public int IdNotification { get; set; }

    public string NomClient { get; set; }
    public decimal Solde { get; set; }
    public decimal Frais { get; set; }
    public string Code { get; set; }

    public NotifType Type { get; set; }
}