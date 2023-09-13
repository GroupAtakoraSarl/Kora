using Kora.Server.Data;
using Microsoft.EntityFrameworkCore;
using Kora.Shared.Models;
using Kora.Shared.ModelsDto;
using Microsoft.VisualBasic;
using MongoDB.Bson.IO;

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

    public void SmsSend(string from, string to, string text, int reference, string api_key, string api_secret)
    {
        from = "KORATRANS";
        to = to;
        text = text;
        reference = reference;
        api_key = api_key;
        api_secret = api_secret;
    }


    public async Task EnregistrerClient(Shared.Models.Client client)
    {
        try
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private bool CheckPassword(string password, string dataPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, dataPassword);
    }
    
    public AuthLogin ConnecterClient(string tel, string password)
    {
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Tel == tel);
        var lecompte = _dbContext.Comptes.FirstOrDefault(c => c.NumCompte == tel);
        
        
        if (leclient is not null &&  CheckPassword(password, leclient.Password))
        {
            var operations = _dbContext.Transactions
                .Where(o => o.NumExp == tel || o.NumDes == tel)
                .Select(t=> new Transaction
                {
                    Solde = t.Solde,
                    NumDes = t.NumDes,
                    NumExp = t.NumExp,
                    Date = t.Date,
                    Frais = t.Frais,
                    Type = t.Type,
                    IdTransaction = t.IdTransaction,
                    IdCompte = lecompte.IdCompte,
                })
                .ToList();
            
            return new AuthLogin
            {
                Tel = leclient.Tel,
                Solde = lecompte.Solde,
                Transactions = operations,
                Username = leclient.Username
            };
        }
        else
        {
            return null;
        }
        
    }
    
    public bool UpdateClient(string password, string newPassword, string tel)
    {
        var existingClient = _dbContext.Clients.FirstOrDefault(c => c.Tel == tel);
        if (existingClient is not null && CheckPassword(password, existingClient.Password))
        {
            var hashNew = BCrypt.Net.BCrypt.HashPassword(newPassword);
            existingClient.Password = hashNew;
        }
        else
        {
            return false;
        }
        

        _dbContext.SaveChangesAsync();
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