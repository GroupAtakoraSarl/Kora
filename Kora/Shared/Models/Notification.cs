using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.Models;

public class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdNotification { get; set; }
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