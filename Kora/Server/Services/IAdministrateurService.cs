using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IAdministrateurService
{
    Task<List<Administrateur>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    bool Enregistrer(Administrateur administrateur);
    bool EnregistrerAdmin(Administrateur administrateur);
    bool EnregistrerAdminSaved(string email, string username, string password);
    bool ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}