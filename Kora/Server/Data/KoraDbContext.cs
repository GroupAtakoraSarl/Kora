using Microsoft.EntityFrameworkCore;
using Kora.Shared.Models;


namespace Kora.Server.Data;

public class KoraDbContext : DbContext
{
    
    public KoraDbContext(DbContextOptions<KoraDbContext> options)
        : base(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agence>()
            .HasOne(a => a.ResponsableAgence)
            .WithMany()
            .HasForeignKey(a => a.IdResponsable)
            .IsRequired();

        modelBuilder.Entity<Ville>()
            .HasOne(v => v.Pays)
            .WithMany()
            .HasForeignKey(v => v.IdPays)
            .IsRequired();

        modelBuilder.Entity<Compte>()
            .HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey(c => c.IdClient)
            .IsRequired();

        modelBuilder.Entity<Kiosque>()
            .HasOne(k => k.Agence)
            .WithMany()
            .HasForeignKey(k=>k.IdAgence)
            .IsRequired();

    }
    
}