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

        public async Task<List<CompteDto>> GetAllComptes()
        {
            return await _httpClient.GetFromJsonAsync<List<CompteDto>>("api/Compte");
        }

        public async Task<CompteDto> GetCompteByNum(string numCompte)
        {
            return await _httpClient.GetFromJsonAsync<CompteDto>($"api/Compte/{numCompte}");
        }

        public async Task<Compte> AddCompte(Compte compte, int idClient)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Compte/CreerCompte?idClient={idClient}", compte);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Compte>();
        }

        // public async Task<bool> DepotCompte(string numCompteExpediteur, string passwordExpediteur, string numCompteDestinataire, decimal solde)
        // {
        //     var queryString = $"?numCompteExpediteur={numCompteExpediteur}&passwordExpediteur={passwordExpediteur}&numCompteDestinataire={numCompteDestinataire}&solde={solde}";
        //     var response = await _httpClient.PostAsJsonAsync($"api/Compte/DepotCompte{queryString}", null).ConfigureAwait(false);
        //     return response.IsSuccessStatusCode;
        // }
        //
        // public async Task<bool> RetraitCompte(string numCompte, decimal solde, string password)
        // {
        //     var response = await _httpClient.PostAsJsonAsync($"api/Compte/RetraitCompte?numCompte={numCompte}&solde={solde}&password={password}", null).ConfigureAwait(false);
        //     return response.IsSuccessStatusCode;
        // }

        // public async Task<bool> Transfert(string numCompte, decimal solde)
        // {
        //     var response = await _httpClient.PostAsJsonAsync($"api/Compte/Transfert?numCompte={numCompte}&solde={solde}", null);
        //     return response.IsSuccessStatusCode;
        // }

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
