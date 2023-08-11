using Kora.Shared.Models;
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

        public async Task<List<Pays>> GetAllPays()
        {
            return await _httpClient.GetFromJsonAsync<List<Pays>>("api/Pays/GetAllPays");
        }

        public async Task<Pays> GetPaysNameById(int idPays)
        {
            return await _httpClient.GetFromJsonAsync<Pays>($"api/Pays/GetPaysNameById/{idPays}");
        }

        public async Task<Pays> AddPays(Pays pays)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Pays/AddPays", pays);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Pays>();
        }

        public async Task<bool> DeletePays(int indicatif)
        {
            var response = await _httpClient.DeleteAsync($"api/Pays/DeletePays/{indicatif}");
            return response.IsSuccessStatusCode;
        }
    }
}