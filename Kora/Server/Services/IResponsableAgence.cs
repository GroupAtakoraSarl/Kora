using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IResponsableAgence
{
    Task<List<ResponsableAgenceDto>> GetAllResponsable();
    Task<ResponsableAgenceDto> GetResponsableByTel(string tel);
    Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence);
    Task<bool> DeleteResponsable(string tel);
}