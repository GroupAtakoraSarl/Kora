using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IResponsableAgence
{
    Task<List<ResponsableAgenceDto>> GetAllResponsable();
    Task<bool> DeleteResponsable(int tel);
}