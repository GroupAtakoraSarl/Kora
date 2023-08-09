using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IAdministrateurService
{
    Task<List<AdministrateurDto>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    Task<bool> EnregistrerAdmin(Administrateur administrateur);
    Task<bool> ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}