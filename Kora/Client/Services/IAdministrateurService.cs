using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IAdministrateurService
{
    Task<List<Administrateur>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    Task<Administrateur> Enregistrer(Administrateur administrateur);
    Task<Administrateur> EnregistrerAdmin(Administrateur administrateur);
    Task<bool> EnregistrerAdminSaved(string email, string username, string password);
    Task<bool> ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}