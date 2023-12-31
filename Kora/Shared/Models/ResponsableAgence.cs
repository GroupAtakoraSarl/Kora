using System.ComponentModel.DataAnnotations;

namespace Kora.Shared.Models;

public class ResponsableAgence
{
    [Key] public int IdResponsable { get; set; }

    public string NomResponsable { get; set; }

    public string PrenomResponsable { get; set; }

    public string SexeResponsable { get; set; }

    public int AgeResponsable { get; set; }

    public string Tel { get; set; }
    public string StatutResponsable { get; set; }
}