using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class KiosqueService : IKiosqueService
{
    private readonly KoraDbContext _dbContext;

    public KiosqueService(IMapper mapper, KoraDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Kiosque>> GetAllKiosque()
    {
        var kiosques = await _dbContext.Kiosques.ToListAsync();
        return kiosques;
    }
    
    public async Task<string> GenerateRandomCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        
        while (await _dbContext.Kiosques.AnyAsync(k => k.Code == code))
        {
            code = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        return code;
    }

    public async Task<string> GenerateRandomKey()
    {
        const string chars = "abcdefghijklmnopkrstuvwxyz123456789";
        var random = new Random();
        var key = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());

        while (await _dbContext.Kiosques.AnyAsync(k=>k.Key == key))
        {
            key = new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        return key;
    }
    
    
    public async Task<List<Kiosque>> GetKiosqueByAdresse(string adresseKiosque)
    {
        var kiosque = await _dbContext.Kiosques
            .Where(k => k.AdresseKiosque == adresseKiosque)
            .ToListAsync();

        return kiosque;
    }
    
    public async Task<Kiosque> AddKiosque(Kiosque kiosque)
    {
        var newKiosque = new Kiosque
        {
            ContactKiosque = kiosque.ContactKiosque,
            NomKiosque = kiosque.NomKiosque,
            Password = "default",
            Code = kiosque.Code = await GenerateRandomCode(),
            Key = kiosque.Key = await GenerateRandomKey(),
            Solde = kiosque.Solde,
            AdresseKiosque = kiosque.AdresseKiosque,
            Agence = kiosque.Agence,
            IdAgence = kiosque.IdAgence,
            IdKiosque = kiosque.IdKiosque
        };
        
        _dbContext.Kiosques.Add(newKiosque);
        await _dbContext.SaveChangesAsync();
        return kiosque;
    }

    public bool EnregistrerKiosque(string nomKiosque, string codeKiosque, string password)
    {
        var hashedPwd = BCrypt.Net.BCrypt.HashPassword(password);
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k =>
            k.NomKiosque == nomKiosque && k.Code == codeKiosque);
        if (kiosque == null)
        {
            return false;
        }
        
        kiosque.Password = hashedPwd;
        _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<KiosqueDto> ConnecterKiosque(string codeKiosque, string password)
    {
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k => k.Code == codeKiosque && k.Password == password);
        if (kiosque == null)
        {
            return null;
        }

        var newKiosque = new KiosqueDto
        {
            NomKiosque = kiosque.NomKiosque,
            Solde = kiosque.Solde,
            AdresseKiosque = kiosque.AdresseKiosque,
            Code = kiosque.Code,
            ContactKiosque = kiosque.ContactKiosque
        };
        return newKiosque;
    }

    public async Task<bool> ChargeSolde(decimal solde, string contactKiosque)
    {
        var kiosque = await _dbContext.Kiosques.FirstOrDefaultAsync(a => a.ContactKiosque == contactKiosque);
        if (kiosque is not null)
        {
            kiosque.Solde += solde;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        else
        {
            return false;
        }

    }
    
    public async Task<bool> DeleteKiosque(string contactKiosque)
    {
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k=>k.ContactKiosque == contactKiosque);
        if (kiosque is null)
        {
            return false;
        }

        _dbContext.Kiosques.Remove(kiosque);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}