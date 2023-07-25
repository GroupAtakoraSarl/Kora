using Kora.Models;
using Kora.Server.ModelsDto;

namespace Kora.Server.Services;

public interface IAdministrateurService
{
    Task<List<AdministrateurDto>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    void EnregistrerAdmin(Administrateur administrateur);
    bool ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}