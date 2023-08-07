using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IAdministrateurService
{
    Task<List<AdministrateurDto>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    bool EnregistrerAdmin(Administrateur administrateur);
    bool ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}