using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public interface IClientService
{
    Task<List<ClientDto>> GetAllClient();
    Task<ClientDto> GetClientByTel(string tel);
    Task<ClientLog> GetClient(string username, string password);
    void EnregistrerClient(Kora.Shared.Models.Client client);
    Task<bool> ConnecterClient(string username, string password);
    Task<bool> UpateClient(string tel,Kora.Shared.Models.Client client);
    Task<bool> DeleteClient(string tel);

}