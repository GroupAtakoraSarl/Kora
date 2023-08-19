using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IAdministrateurService
{
    Task<List<Administrateur>> GetAllAdmin();
    Task<AdministrateurDto> GetAdminByEmail(string email);
    Task<Administrateur> Enregistrer(AdministrateurDto administrateur);
    Task<Administrateur> EnregistrerAdmin(Administrateur administrateur);
    Task<bool> EnregistrerAdminSaved(string username, string email, string password);
    Task<HttpResponseMessage> ConnecterAdmin(string email, string password);
    Task<bool> DeleteAdmin(string email);
}