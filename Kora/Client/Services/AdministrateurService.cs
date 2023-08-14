using System.Net.Http.Json;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;

namespace Kora.Client.Services;

public class AdministrateurService : IAdministrateurService
{
    private readonly HttpClient _http;

    public AdministrateurService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<Administrateur>> GetAllAdmin()
    {
        return await _http.GetFromJsonAsync<List<Administrateur>>("api/Administrateur");
    }

    public async Task<AdministrateurDto> GetAdminByEmail(string email)
    {
        return await _http.GetFromJsonAsync<AdministrateurDto>($"api/Administrateur/GetAdminByEmail/{email}");
    }

    public async Task<Administrateur> Enregistrer(AdministrateurDto administrateur)
    {
        var response = await _http.PostAsJsonAsync("api/Administrateur/Enregistrer", administrateur);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Administrateur>();
    }

    public async Task<Administrateur> EnregistrerAdmin(Administrateur administrateur)
    {
        var response = await _http.PostAsJsonAsync("api/Administrateur/EnregistrerAdmin", administrateur);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Administrateur>();
    }

    public async Task<bool> EnregistrerAdminSaved(string username, string email, string password)
    {
        var admin = new Administrateur {  Username = username, Email = email, Password = password };
        var response = await _http.PostAsJsonAsync("api/Administrateur/EnregistrerAdminSaved", admin);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ConnecterAdmin(string email, string password)
    {
        var administrateur = new Administrateur { Email = email, Password = password };
        var response = await _http.PostAsJsonAsync("api/Administrateur/ConnecterAdmin", administrateur);
        return response.IsSuccessStatusCode;

    }


    public async Task<bool> DeleteAdmin(string email)
    {
        var response = await _http.DeleteAsync($"api/Administrateur/DeleteAdmin/{email}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EnregistrerAdmin(string adminUsername, string adminEmail, string adminPassword)
    {
        throw new NotImplementedException();
    }

    // Implémentez les autres méthodes en utilisant HttpClient
}