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
            return kiosque;
        }

        public async Task<bool> ChargeSolde(double solde, string contactAgence)
        {
            var chargeSoldeRequest = new
            {
                ContactAgence = contactAgence,
                Solde = solde
            };

            var response = await _httpClient.PostAsJsonAsync("api/Kiosque/ChargeSolde", chargeSoldeRequest);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> DeleteKiosque(int contactKiosque)
        {
            var response = await _httpClient.DeleteAsync($"api/Kiosque/DeleteKiosque/{contactKiosque}");
            return response.IsSuccessStatusCode;
        }
    }
}