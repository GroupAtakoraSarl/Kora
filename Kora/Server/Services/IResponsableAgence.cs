using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IResponsableAgence
{
    Task<List<ResponsableAgence>> GetAllResponsable();
    Task<ResponsableAgenceDto> GetResponsableByTel(string tel);
    Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence);
    Task<bool> DeleteResponsable(string tel);
}