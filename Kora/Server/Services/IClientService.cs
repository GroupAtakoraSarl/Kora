using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Server.Services;

public interface IClientService
{
    Task<List<Shared.Models.Client>> GetAllClient();
    Task<Shared.Models.Client> GetClientByTel(string tel);
    // Task<ClientLog> GetClient(string username, string password);
    Task EnregistrerClient(Shared.Models.Client client);
    AuthLogin ConnecterClient(string tel, string password);
    Task<bool> UpateClient(string tel,Shared.Models.Client client);
    Task<bool> DeleteClient(string tel);
    
}
