using System.Net.Http.Json;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services
{
    public class AgenceService : IAgenceService
    {
        private readonly HttpClient _httpClient;

        public AgenceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AgenceDto>> GetAllAgence()
        {
            return await _httpClient.GetFromJsonAsync<List<AgenceDto>>("api/Agence");
        }
        
        public async Task<AgenceDto> GetAgenceByContact(string contactAgence)
        {
            return await _httpClient.GetFromJsonAsync<AgenceDto>($"api/Agence/GetAgenceByContact/{contactAgence}");
        }
        
        public async Task<Agence> AddAgence(Agence agence)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Agence/AddAgence", agence);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Agence>();
        }
        
        public async Task<bool> DeleteAgence(string contactAgence)
        {
            var response = await _httpClient.DeleteAsync($"api/Agence/DeleteAgence/{contactAgence}");
            return response.IsSuccessStatusCode;
        }
    }
}