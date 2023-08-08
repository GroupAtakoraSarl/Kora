namespace Kora.Shared.ModelsDto;

public class AgenceDto
{
    public int IdAgence { get; set; }
    public string Pays { get; set; }
    public string Ville { get; set; }
    public string NomAgence { get; set; }
    public string AdresseAgence { get; set; }
    public string ContactAgence { get; set; }
    public string EmailAgence { get; set; }
    public string DeviseAgence { get; set; }
    public double SoldeInitial { get; set; }
    public int IdResponsable { get; set; }
}