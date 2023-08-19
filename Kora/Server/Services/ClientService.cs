using AutoMapper;
using Kora.Server.Data;
using Kora.Server.Services;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Kora.Shared.Models;

namespace Kora.Server.Services;

public class ClientService : IClientService
{
    private readonly KoraDbContext _dbContext;
    private readonly ICompteService _compteService;
    

    public ClientService(KoraDbContext dbContext, ICompteService compteService)
    {
        _compteService = compteService;
        _dbContext = dbContext;
    }
    
    public async Task<List<Shared.Models.Client>> GetAllClient()
    {
        var clients = await _dbContext.Clients.ToListAsync();
        return clients;
    }

    public async Task<Shared.Models.Client> GetClientByTel(string tel)
    {
        var client = await _dbContext.Clients.FindAsync(tel);
        return client;
    }

    
    public async Task EnregistrerClient(Shared.Models.Client client)
    {
        if (await _dbContext.Clients.AnyAsync(c => c.Username == client.Username))
        {
            throw new Exception("Username already exists");
        }

        if (await _dbContext.Clients.AnyAsync(c => c.Tel == client.Tel))
        {
            throw new Exception("Teléphone already exists");
        }
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(client.Password);
        
        client.Password = hashedPassword;
        Compte compte = new Compte
        {
            IdClient = client.IdClient,
            Solde = 0,
            NumCompte = client.Tel,
            Client = client,
            Transactions = new List<Transaction>()
        };
        
        _dbContext.Clients.Add(client);
        _dbContext.Comptes.Add(compte);
        await _dbContext.SaveChangesAsync();
    }

    
    public Shared.Models.Client ConnecterClient(string tel, string password)
    {
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Tel == tel);
        if (leclient is null)
        {
            return null;
        }

        // Vérifier que le mot de passe fourni correspond au hachage de mot de passe stocké
         var isConnected =  BCrypt.Net.BCrypt.Verify(password, leclient.Password);
         return isConnected ? leclient : null;
    }
    
 
    public async Task<bool> UpateClient(string tel, Shared.Models.Client client)
    {
        var existingClient = await _dbContext.Clients.FindAsync(tel);
        if (existingClient is null)
        {
            return false;
        }

        existingClient.Username = client.Username;
        existingClient.Tel = client.Tel;
        existingClient.Password = client.Password;
        
        await _dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteClient(string tel)
    {
        var client = _dbContext.Clients.FirstOrDefault(c=>c.Tel == tel);
        if (client is null)
            return false;

        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();

        return true;

    }
}