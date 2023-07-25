using AutoMapper;
using Kora.Server.Data;
using Kora.Server.Services;
using Kora.Server.ModelsDto;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using Kora.Models;

namespace Kora.Server.Services;

public class ClientService : IClientService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;


    public ClientService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<List<ClientDto>> GetAllClient()
    {
        var clients = await _dbContext.Administrateurs.ToListAsync();
        return _mapper.Map<List<ClientDto>>(clients);
    }

    public async Task<ClientDto> GetClientByTel(string tel)
    {
        var client = await _dbContext.Clients.FindAsync(tel);
        return _mapper.Map<ClientDto>(client);
    }

    public async Task<ClientLog> GetClient(string username, string password)
    {
        var client = await _dbContext.Clients.FindAsync(username, password);
        return _mapper.Map<ClientLog>(client);
    }
    
    public void EnregistrerClient(Models.Client client)
    {
        if (_dbContext.Clients.Any(c => c.Username == client.Username))
        {
            throw new Exception("Username already exists");
        }
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(client.Password);
        
        var leclient = _mapper.Map<Models.Client>(client);
        client.Password = hashedPassword;
        _dbContext.Clients.Add(leclient);
        _dbContext.SaveChangesAsync();
    }

    
    public bool ConnecterClient(string username, string password)
    {
        var leclient = _dbContext.Clients.FirstOrDefault(c => c.Username == username);
        if (leclient is null)
        {
            return false;
        }

        // Vérifier que le mot de passe fourni correspond au hachage de mot de passe stocké
        return BCrypt.Net.BCrypt.Verify(password, leclient.Password);
    }
    
 
    public async Task<bool> UpateClient(string tel, Models.Client client)
    {
        var existingClient = await _dbContext.Clients.FindAsync(tel);
        if (existingClient is null)
        {
            return false;
        }

        _mapper.Map(client, existingClient);
        await _dbContext.SaveChangesAsync();
        return true;
    }


    public async Task<bool> DeleteClient(string tel)
    {
        var client = await _dbContext.Clients.FindAsync(tel);
        if (client is null)
            return false;

        _dbContext.Clients.Remove(client);
        await _dbContext.SaveChangesAsync();

        return true;

    }
}