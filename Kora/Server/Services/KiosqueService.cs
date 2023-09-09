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
        const string chars = "abchijklopkrstuvwxyz123456789";
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
        kiosque.Code = await GenerateRandomCode();
        kiosque.Key = await GenerateRandomKey();
        _dbContext.Kiosques.Add(kiosque);
        await _dbContext.SaveChangesAsync();
        return kiosque;
    }

    public bool EnregistrerKiosque(string nomKiosque, string keyKiosque, string password)
    {
        var hashedPwd = BCrypt.Net.BCrypt.HashPassword(password);
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k =>
            k.NomKiosque == nomKiosque && k.Key == keyKiosque);
        if (kiosque == null)
        {
            return false;
        }
        
        kiosque.Password = hashedPwd;
        _dbContext.SaveChangesAsync();
        return true;
    }
    
    private bool CheckPassword(string password, string dataPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, dataPassword);
    }

    public KiosqueTrans ConnecterKiosque(string codeKiosque, string password)
    {
        
        var kiosque = _dbContext.Kiosques.FirstOrDefault(k => k.Code == codeKiosque);
        
        if (kiosque != null && CheckPassword(password, kiosque.Password))
        {
            var operations = _dbContext.Transactions
                .Where(k => k.NumDes == kiosque.Code || k.NumExp == kiosque.Code)
                .Select(t => new Transaction
                {
                    Solde = t.Solde, 
                    NumDes = t.NumDes,
                    NumExp = t.NumExp,
                    Date = t.Date,
                    Frais = t.Frais,
                    Type = t.Type,
                    IdCompte = t.IdCompte,
                    IdTransaction = t.IdTransaction,
                    Compte = t.Compte
                });
            return new KiosqueTrans
            {
                IdKiosque = kiosque.IdKiosque,
                NomKiosque = kiosque.NomKiosque,
                ContactKiosque = kiosque.ContactKiosque,
                Code = kiosque.Code,
                Solde = kiosque.Solde,
                AdresseKiosque = kiosque.AdresseKiosque,
                Transactions = operations
            };
        }

        return null;

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

    public async Task<decimal?> GetKiosqueSolde(string code)
    {
        var lekiosque = _dbContext.Kiosques.FirstOrDefault(k => k.Code == code);
        if (lekiosque != null)
        {
            var kiosque = _dbContext.Kiosques.FirstOrDefault(k => k.IdKiosque == lekiosque.IdKiosque);
            if (kiosque != null)
            {
                return lekiosque.Solde;
            }
        }
        return null;
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