using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using System.Net.Http.Json;

namespace Kora.Client.Services
{
    public class ResponsableAgenceService : IResponsableAgence
    {
        private readonly HttpClient _httpClient;

        public ResponsableAgenceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResponsableAgenceDto>> GetAllResponsable()
        {
            return await _httpClient.GetFromJsonAsync<List<ResponsableAgenceDto>>("api/ResponsableAgence");
        }

        public async Task<ResponsableAgenceDto> GetResponsableByTel(string tel)
        {
            return await _httpClient.GetFromJsonAsync<ResponsableAgenceDto>($"api/ResponsableAgence/GetResponsableByTel/{tel}");
        }

        public async Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ResponsableAgence/AddResponsable", responsableAgence);
            response.EnsureSuccessStatusCode();
            return responsableAgence;
        }

        public async Task<bool> DeleteResponsable(string tel)
        {
            var response = await _httpClient.DeleteAsync($"api/ResponsableAgence/DeleteResponsable/{tel}");
            return response.IsSuccessStatusCode;
        }
    }
}