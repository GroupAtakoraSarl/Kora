using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using System.Net.Http.Json;

namespace Kora.Client.Services
{
    public class CompteService : ICompteService
    {
        private readonly HttpClient _httpClient;

        public CompteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Compte>> GetAllComptes()
        {
            return await _httpClient.GetFromJsonAsync<List<Compte>>("api/Compte");
        }

        public async Task<CompteDto> GetCompteByNum(string numCompte)
        {
            return await _httpClient.GetFromJsonAsync<CompteDto>($"api/Compte/{numCompte}");
        }

        public async Task<List<CompteDto>> GetCompteByClientId(int clientId)
        {
            return await _httpClient.GetFromJsonAsync<List<CompteDto>>($"api/Compte/GetCompteByClientId/{clientId}");
        }

        

        public async Task<bool> DeleteCompte(string numCompte)
        {
            var response = await _httpClient.DeleteAsync($"api/Compte/{numCompte}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<string>> GetTransaction()
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("api/Compte/GetTransaction");
        }
    }
}
