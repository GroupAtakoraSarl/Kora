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

        public async Task<bool> ChargeSolde(ChargeCompteDto chargeCompteDto)
        {
           // var requestData = new { Solde = solde, ContactKiosque = contactKiosque };
            var response = await _httpClient.PostAsJsonAsync("api/Kiosque/ChargeSolde", chargeCompteDto);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
        public async Task<bool> DeleteKiosque(KiosqueDeleteDto kiosqueDeleteDto)
        {
            var response = await _httpClient.DeleteAsync($"api/Kiosque/DeleteKiosque/{kiosqueDeleteDto}");
            return response.IsSuccessStatusCode;
        }
    }
}