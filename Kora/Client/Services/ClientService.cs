using System.Net.Http.Json;
using Kora.Shared.ModelsDto;
using Kora.Shared.Models;

namespace Kora.Client.Services;

public class ClientService : IClientService
{
    private readonly HttpClient _httpClient;
    

    public ClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ClientDto>> GetAllClient()
    {
        return await _httpClient.GetFromJsonAsync<List<ClientDto>>("api/Client/GetAllClient");
    }

    public async Task<ClientDto> GetClientByTel(string tel)
    {
        return await _httpClient.GetFromJsonAsync<ClientDto>($"api/Client/GetClientByTel/{tel}");
    }
    
    public async Task<ClientLog> GetClient(string username, string password)
    {
        return await _httpClient.GetFromJsonAsync<ClientLog>($"api/Client/GetClient/{username}/{password}");
    }

    public async void EnregistrerClient(Kora.Shared.Models.Client client)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Client/EnregistrerClient", client);
        response.EnsureSuccessStatusCode();
    }

    public async Task<bool> ConnecterClient(string username, string password)
    {
        var response = await _httpClient.PostAsJsonAsync<Kora.Shared.Models.Client>($"api/Client/ConnecterClient/{username}/{password}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpateClient(string tel, Kora.Shared.Models.Client client)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Client/UpdateClient/{tel}", client);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<bool> DeleteClient(string tel)
    {
        var response = await _httpClient.DeleteAsync($"api/Client/DeleteClient/{tel}");
        return response.IsSuccessStatusCode;
    }
}