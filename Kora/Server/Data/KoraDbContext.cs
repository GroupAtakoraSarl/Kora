using Microsoft.EntityFrameworkCore;
using Kora.Models;


namespace Kora.Server.Data;

public class KoraDbContext : DbContext
{
    
    public KoraDbContext(DbContextOptions<KoraDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agence>()
            .HasOne(a => a.ResponsableAgence)
            .WithMany()
            .HasForeignKey("IdResponsable")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ville>()
            .HasOne(v => v.Pays)
            .WithMany()
            .HasForeignKey("IdPays")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Compte>()
            .HasOne(c => c.Client)
            .WithMany()
            .HasForeignKey("IdClient")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Compte)
            .WithMany(c => c.Transactions)
            .HasForeignKey("IdCompte")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Kiosque>()
            .HasOne(k => k.Agence)
            .WithMany()
            .HasForeignKey("IdAgence")
            .OnDelete(DeleteBehavior.Restrict);

    }
    
    public DbSet<Agence> Agences { get; set; }
    public DbSet<Administrateur> Administrateurs { get; set; }
    public DbSet<Models.Client> Clients { get; set; }
    public DbSet<Compte> Comptes { get; set; }
    public DbSet<Kiosque> Kiosques { get; set; }
    public DbSet<Pays> Pays { get; set; }
    public DbSet<ResponsableAgence> ResponsableAgences { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Ville> Villes { get; set; }
    

}