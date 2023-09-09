using System.ComponentModel.DataAnnotations;

namespace Kora.Shared.Models;

public class NotificationKiosque
{
    public enum NotifType
    {
        Dépôt
    }

    [Key] public int IdNotification { get; set; }

    public string Code { get; set; }
    public string NomClient { get; set; }
    public decimal Solde { get; set; }
    public decimal Frais { get; set; }

    public NotifType Type { get; set; }
}