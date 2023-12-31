using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IClientService
{
    Task<List<Shared.Models.Client>> GetAllClient();
    Task<Shared.Models.Client> GetClientByTel(string tel);
    Task EnregistrerClient(Shared.Models.Client client);
    AuthLogin ConnecterClient(string tel, string password);
    bool UpdateClient(string password, string newPassword, string tel);
    Task<bool> DeleteClient(string tel);
    
}
