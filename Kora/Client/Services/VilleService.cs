using Kora.Shared.Models;
using System.Net.Http.Json;

namespace Kora.Client.Services
{
    public class VilleService : IVilleService
    {
        private readonly HttpClient _httpClient;

        public VilleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Ville> AddVille(Ville ville)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Ville/AddVille", ville);
            response.EnsureSuccessStatusCode();
            return ville;
        }

        public async Task<bool> DeleteVille(string nomVille)
        {
            var response = await _httpClient.DeleteAsync($"api/Ville/DeleteVille/{nomVille}");
            return response.IsSuccessStatusCode;
        }
    }
}