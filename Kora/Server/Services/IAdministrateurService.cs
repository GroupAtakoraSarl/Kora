using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IAdministrateurService
{
    Task<List<Administrateur>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    Task<Administrateur> Enregistrer(Administrateur administrateur);
    bool EnregistrerAdmin(Administrateur administrateur);
    bool EnregistrerAdminSaved(string username, string email, string password);
    bool ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}