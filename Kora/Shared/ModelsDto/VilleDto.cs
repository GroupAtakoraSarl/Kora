using System.ComponentModel.DataAnnotations.Schema;

namespace Kora.Shared.ModelsDto;

public class VilleDto
{
    public int IdVille { get; set; }
    public string NomVille { get; set; }
    public int IdPays { get; set; }
}

