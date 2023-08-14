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

    public async Task<Administrateur> Enregistrer(Administrateur administrateur)
    {
        var adminUsername = administrateur.Username;
        var adminEmail = administrateur.Email;
        
        var newAdmin = new Administrateur
        {
            Username = adminUsername,
            Email = adminEmail,
            Password = "default"
        };
        _dbContext.Administrateurs.Add(newAdmin);
         await _dbContext.SaveChangesAsync();

        return newAdmin;
    }


    public bool EnregistrerAdmin(Administrateur administrateur)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(administrateur.Password);
        
        administrateur.Password = hashedPassword;
        _dbContext.Administrateurs.Add(administrateur);
        _dbContext.SaveChangesAsync();
        
        return true;
    }

    public bool EnregistrerAdminSaved(string username, string email, string password)
    {
        string hashedPwd = BCrypt.Net.BCrypt.HashPassword(password);
        var adminSaved = _dbContext.Administrateurs.FirstOrDefault(a => a.Username == username && a.Email == email);
        if (adminSaved == null)
        {
            return false;
        }
        adminSaved.Password = hashedPwd;
        _dbContext.SaveChanges();
        return true;
    }

    public bool ConnecterAdmin(string email, string password)
    {
        var ladmin = _dbContext.Administrateurs.FirstOrDefault(a => a.Email == email);
        if (ladmin == null)
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