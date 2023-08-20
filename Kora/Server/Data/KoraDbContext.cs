using Microsoft.EntityFrameworkCore;
using Kora.Shared.Models;


namespace Kora.Server.Data;

public class KoraDbContext : DbContext
{
    
    public KoraDbContext(DbContextOptions<KoraDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Agence> Agences { get; set; }
    public DbSet<Administrateur> Administrateurs { get; set; }
    public DbSet<Shared.Models.Client> Clients { get; set; }
    public DbSet<Compte> Comptes { get; set; }
    public DbSet<Kiosque> Kiosques { get; set; }
    public DbSet<Pays> Pays { get; set; }
    public DbSet<ResponsableAgence> ResponsableAgences { get; set; }
    public DbSet<Ville> Villes { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<Agence>()
        //     .HasOne(a => a.ResponsableAgence)
        //     .WithMany()
        //     .HasForeignKey(a => a.IdResponsable);
        //
        // modelBuilder.Entity<Pays>()
        //     .HasMany(p => p.Villes)
        //     .WithOne(v => v.Pays)
        //     .HasForeignKey(v => v.IdPays);
        //
        // modelBuilder.Entity<Compte>()
        //     .HasOne(c => c.Client)
        //     .WithMany(c => c.Comptes)
        //     .HasForeignKey(c => c.IdClient);
        //
        // modelBuilder.Entity<Kora.Shared.Models.Transaction>()
        //     .HasOne(t => t.Compte)
        //     .WithMany(c => c.Transactions)
        //     .HasForeignKey(t => t.IdCompte);
        //
        // modelBuilder.Entity<Kiosque>()
        //     .HasOne(k => k.Agence)
        //     .WithMany(a => a.Kiosques)
        //     .HasForeignKey(k => k.IdAgence);
        //
    }
}

