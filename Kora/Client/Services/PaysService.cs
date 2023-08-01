using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using System.Net.Http.Json;

namespace Kora.Client.Services
{
    public class PaysService : IPaysService
    {
        private readonly HttpClient _httpClient;

        public PaysService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PaysDto>> GetAllPays()
        {
            return await _httpClient.GetFromJsonAsync<List<PaysDto>>("api/Pays");
        }

        public async Task<PaysDto> GetPaysByIndi(int indicatif)
        {
            return await _httpClient.GetFromJsonAsync<PaysDto>($"api/Pays/GetPaysByIndi/{indicatif}");
        }

        public async Task<Pays> AddPays(Pays pays)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pays/AddPays", pays);
            response.EnsureSuccessStatusCode();
            return pays;
        }

        public async Task<bool> DeletePays(int indicatif)
        {
            var response = await _httpClient.DeleteAsync($"api/Pays/DeletePays/{indicatif}");
            return response.IsSuccessStatusCode;
        }
    }
}