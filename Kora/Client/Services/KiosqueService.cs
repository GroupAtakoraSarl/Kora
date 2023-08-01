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

        public async Task<List<KiosqueDto>> GetAllKiosque()
        {
            return await _httpClient.GetFromJsonAsync<List<KiosqueDto>>("api/Kiosque");
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

        public async Task<bool> DeleteKiosque(int contactKiosque)
        {
            var response = await _httpClient.DeleteAsync($"api/Kiosque/DeleteKiosque/{contactKiosque}");
            return response.IsSuccessStatusCode;
        }
    }
}