using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using System.Net.Http.Json;

namespace Kora.Client.Services
{
    public class KiosqueService : IKiosqueService
    {
        private readonly HttpClient _httpClient;

        public KiosqueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Kiosque>> GetAllKiosque()
        {
            return await _httpClient.GetFromJsonAsync<List<Kiosque>>("api/Kiosque/GetAllKiosque");
        }

        public async Task<List<KiosqueDto>> GetKiosqueByAdresse(string adresseKiosque)
        {
            return await _httpClient.GetFromJsonAsync<List<KiosqueDto>>($"api/Kiosque/GetKiosqueByAdresse/{adresseKiosque}");
        }

        public async Task<Kiosque> AddKiosque(Kiosque kiosque)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Kiosque/AddKiosque", kiosque);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Kiosque>();
        }

        public async Task<bool> ChargeSolde(decimal solde, string contactKiosque)
        {
            var requestUri = $"api/Kiosque/ChargeSolde?contactKiosque={contactKiosque}&solde={solde}";
            var response = await _httpClient.PostAsync(requestUri, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw new Exception($"Erreur lors de l'appel de l'API : {response.ReasonPhrase}");
            }
        }
        
        
        
        public async Task<bool> DeleteKiosque(int contactKiosque)
        {
            var response = await _httpClient.DeleteAsync($"api/Kiosque/DeleteKiosque/{contactKiosque}");
            return response.IsSuccessStatusCode;
        }
    }
}