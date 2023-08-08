using AutoMapper;
using Kora.Shared.Models;
using Kora.Server.Data;
using Kora.Shared.ModelsDto;
using Microsoft.EntityFrameworkCore;

namespace Kora.Server.Services;

public class AdministrateurService : IAdministrateurService
{
    private readonly IMapper _mapper;
    private readonly KoraDbContext _dbContext;

    public AdministrateurService(IMapper mapper, KoraDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<List<Administrateur>> GetAllAdmin()
    {
        var admins = await _dbContext.Administrateurs.ToListAsync();
        return admins;
    }

    public async Task<AdministrateurDto> GetAdminByEmail(string email)
    {
        var admin = await _dbContext.Administrateurs.FirstOrDefaultAsync(a=>a.Email == email);
        return _mapper.Map<AdministrateurDto>(admin);

    }

    public bool EnregistrerAdmin(Administrateur administrateur)
    {
        if (_dbContext.Administrateurs.Any(a => a.Username == administrateur.Username))
        {
            return false;
        }
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(administrateur.Password);
        
        var ladmin = _mapper.Map<Administrateur>(administrateur);
        administrateur.Password = hashedPassword;
        _dbContext.Administrateurs.Add(ladmin);
        _dbContext.SaveChangesAsync();
        
        return true;
    }


    public bool ConnecterAdmin(string email, string password)
    {
        var ladmin = _dbContext.Administrateurs.FirstOrDefault(a => a.Email == email);
        if (ladmin is null)
        {
            return false;
        }

        // Vérifier que le mot de passe fourni correspond au hachage de mot de passe stocké
        return BCrypt.Net.BCrypt.Verify(password, ladmin.Password);
    }
    
    public async Task<bool> DeleteAdmin(string email)
    {
        var admin = await _dbContext.Administrateurs.FindAsync(email);
        if (admin is null)
        {
            return false;
        }

        _dbContext.Administrateurs.Remove(admin);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}