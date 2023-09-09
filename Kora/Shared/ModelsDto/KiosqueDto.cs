using System.ComponentModel.DataAnnotations.Schema;
using Kora.Shared.Models;

namespace Kora.Shared.ModelsDto;

public class KiosqueDto
{
    public int IdKiosque { get; set; }
    public string NomKiosque { get; set; }
    public string Code { get; set; }
    public string Password { get; set; }
    public string Key { get; set; }
    public decimal Solde { get; set; }
    public string AdresseKiosque { get; set; }
    public string ContactKiosque { get; set; }
    public int IdAgence { get; set; }

}