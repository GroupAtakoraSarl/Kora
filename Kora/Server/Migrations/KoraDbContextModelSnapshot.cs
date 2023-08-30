﻿// <auto-generated />
using System;
using Kora.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kora.Server.Migrations
{
    [DbContext(typeof(KoraDbContext))]
    partial class KoraDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0-preview.6.23329.4");

            modelBuilder.Entity("Kora.Shared.Models.Administrateur", b =>
                {
                    b.Property<int>("IdAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdAdmin");

                    b.ToTable("Administrateurs");
                });

            modelBuilder.Entity("Kora.Shared.Models.Agence", b =>
                {
                    b.Property<int>("IdAgence")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdresseAgence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactAgence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAgence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdResponsable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomAgence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdAgence");

                    b.HasIndex("IdResponsable");

                    b.ToTable("Agences");
                });

            modelBuilder.Entity("Kora.Shared.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdClient");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Kora.Shared.Models.Compte", b =>
                {
                    b.Property<int>("IdCompte")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdClient")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumCompte")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Solde")
                        .HasColumnType("TEXT");

                    b.HasKey("IdCompte");

                    b.HasIndex("IdClient");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("Kora.Shared.Models.Kiosque", b =>
                {
                    b.Property<int>("IdKiosque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdresseKiosque")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactKiosque")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("IdAgence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomKiosque")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Solde")
                        .HasColumnType("TEXT");

                    b.HasKey("IdKiosque");

                    b.HasIndex("IdAgence");

                    b.ToTable("Kiosques");
                });

            modelBuilder.Entity("Kora.Shared.Models.NotificationClient", b =>
                {
                    b.Property<int>("IdNotification")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Frais")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomClient")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Solde")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdNotification");

                    b.ToTable("NotificationsClients");
                });

            modelBuilder.Entity("Kora.Shared.Models.NotificationKiosque", b =>
                {
                    b.Property<int>("IdNotification")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Frais")
                        .HasColumnType("TEXT");

                    b.Property<string>("NomClient")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Solde")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdNotification");

                    b.ToTable("NotificationKiosques");
                });

            modelBuilder.Entity("Kora.Shared.Models.Pays", b =>
                {
                    b.Property<int>("IdPays")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CodeISO")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("DevisePays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Indicatif")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomPays")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdPays");

                    b.ToTable("Pays");
                });

            modelBuilder.Entity("Kora.Shared.Models.ResponsableAgence", b =>
                {
                    b.Property<int>("IdResponsable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AgeResponsable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomResponsable")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PrenomResponsable")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SexeResponsable")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StatutResponsable")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdResponsable");

                    b.ToTable("ResponsableAgences");
                });

            modelBuilder.Entity("Kora.Shared.Models.Transaction", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Frais")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdCompte")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumDes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NumExp")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Solde")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdTransaction");

                    b.HasIndex("IdCompte");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Kora.Shared.Models.Ville", b =>
                {
                    b.Property<int>("IdVille")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdPays")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomVille")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdVille");

                    b.HasIndex("IdPays");

                    b.ToTable("Villes");
                });

            modelBuilder.Entity("Kora.Shared.Models.Agence", b =>
                {
                    b.HasOne("Kora.Shared.Models.ResponsableAgence", "ResponsableAgence")
                        .WithMany()
                        .HasForeignKey("IdResponsable")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ResponsableAgence");
                });

            modelBuilder.Entity("Kora.Shared.Models.Compte", b =>
                {
                    b.HasOne("Kora.Shared.Models.Client", "Client")
                        .WithMany("Comptes")
                        .HasForeignKey("IdClient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Kora.Shared.Models.Kiosque", b =>
                {
                    b.HasOne("Kora.Shared.Models.Agence", "Agence")
                        .WithMany("Kiosques")
                        .HasForeignKey("IdAgence")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agence");
                });

            modelBuilder.Entity("Kora.Shared.Models.Transaction", b =>
                {
                    b.HasOne("Kora.Shared.Models.Compte", "Compte")
                        .WithMany("Transactions")
                        .HasForeignKey("IdCompte")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });

            modelBuilder.Entity("Kora.Shared.Models.Ville", b =>
                {
                    b.HasOne("Kora.Shared.Models.Pays", "Pays")
                        .WithMany("Villes")
                        .HasForeignKey("IdPays")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pays");
                });

            modelBuilder.Entity("Kora.Shared.Models.Agence", b =>
                {
                    b.Navigation("Kiosques");
                });

            modelBuilder.Entity("Kora.Shared.Models.Client", b =>
                {
                    b.Navigation("Comptes");
                });

            modelBuilder.Entity("Kora.Shared.Models.Compte", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Kora.Shared.Models.Pays", b =>
                {
                    b.Navigation("Villes");
                });
#pragma warning restore 612, 618
        }
    }
}
