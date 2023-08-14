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

        public async Task<List<ResponsableAgence>> GetAllResponsable()
        {
            return await _httpClient.GetFromJsonAsync<List<ResponsableAgence>>("api/ResponsableAgence");
        }

        public async Task<ResponsableAgenceDto> GetResponsableByTel(string tel)
        {
            return await _httpClient.GetFromJsonAsync<ResponsableAgenceDto>($"api/ResponsableAgence/GetResponsableByTel/{tel}");
        }

        public async Task<ResponsableAgence> AddResponsable(ResponsableAgence responsableAgence)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ResponsableAgence/AjouterResponsable", responsableAgence);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponsableAgence>();
        }

        public async Task<bool> DeleteResponsable(string tel)
        {
            var response = await _httpClient.DeleteAsync($"api/ResponsableAgence/DeleteResponsable/{tel}");
            return response.IsSuccessStatusCode;
        }
    }
}