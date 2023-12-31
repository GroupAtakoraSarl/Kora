﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kora.Server.Migrations
{
    /// <inheritdoc />
    public partial class koraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrateurs",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateurs", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Tel = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "NotificationKiosques",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    NomClient = table.Column<string>(type: "TEXT", nullable: false),
                    Solde = table.Column<decimal>(type: "TEXT", nullable: false),
                    Frais = table.Column<decimal>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationKiosques", x => x.IdNotification);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsClients",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomClient = table.Column<string>(type: "TEXT", nullable: false),
                    Solde = table.Column<decimal>(type: "TEXT", nullable: false),
                    Frais = table.Column<decimal>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsClients", x => x.IdNotification);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    IdPays = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomPays = table.Column<string>(type: "TEXT", nullable: false),
                    Indicatif = table.Column<int>(type: "INTEGER", nullable: false),
                    CodeISO = table.Column<string>(type: "TEXT", nullable: false),
                    DevisePays = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.IdPays);
                });

            migrationBuilder.CreateTable(
                name: "ResponsableAgences",
                columns: table => new
                {
                    IdResponsable = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomResponsable = table.Column<string>(type: "TEXT", nullable: false),
                    PrenomResponsable = table.Column<string>(type: "TEXT", nullable: false),
                    SexeResponsable = table.Column<string>(type: "TEXT", nullable: false),
                    AgeResponsable = table.Column<int>(type: "INTEGER", nullable: false),
                    Tel = table.Column<string>(type: "TEXT", nullable: false),
                    StatutResponsable = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResponsableAgences", x => x.IdResponsable);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    IdCompte = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumCompte = table.Column<string>(type: "TEXT", nullable: false),
                    Solde = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdClient = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.IdCompte);
                    table.ForeignKey(
                        name: "FK_Comptes_Clients_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Clients",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Villes",
                columns: table => new
                {
                    IdVille = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomVille = table.Column<string>(type: "TEXT", nullable: false),
                    IdPays = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villes", x => x.IdVille);
                    table.ForeignKey(
                        name: "FK_Villes_Pays_IdPays",
                        column: x => x.IdPays,
                        principalTable: "Pays",
                        principalColumn: "IdPays",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Agences",
                columns: table => new
                {
                    IdAgence = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Pays = table.Column<string>(type: "TEXT", nullable: false),
                    Ville = table.Column<string>(type: "TEXT", nullable: false),
                    NomAgence = table.Column<string>(type: "TEXT", nullable: false),
                    AdresseAgence = table.Column<string>(type: "TEXT", nullable: false),
                    ContactAgence = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAgence = table.Column<string>(type: "TEXT", nullable: false),
                    IdResponsable = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agences", x => x.IdAgence);
                    table.ForeignKey(
                        name: "FK_Agences_ResponsableAgences_IdResponsable",
                        column: x => x.IdResponsable,
                        principalTable: "ResponsableAgences",
                        principalColumn: "IdResponsable",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kiosques",
                columns: table => new
                {
                    IdKiosque = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Key = table.Column<string>(type: "TEXT", nullable: false),
                    Solde = table.Column<decimal>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    AdresseKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    ContactKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    IdAgence = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiosques", x => x.IdKiosque);
                    table.ForeignKey(
                        name: "FK_Kiosques_Agences_IdAgence",
                        column: x => x.IdAgence,
                        principalTable: "Agences",
                        principalColumn: "IdAgence",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    IdTransaction = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Solde = table.Column<decimal>(type: "TEXT", nullable: false),
                    NumExp = table.Column<string>(type: "TEXT", nullable: false),
                    NumDes = table.Column<string>(type: "TEXT", nullable: false),
                    Frais = table.Column<decimal>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCompte = table.Column<int>(type: "INTEGER", nullable: false),
                    KiosqueIdKiosque = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.IdTransaction);
                    table.ForeignKey(
                        name: "FK_Transactions_Comptes_IdCompte",
                        column: x => x.IdCompte,
                        principalTable: "Comptes",
                        principalColumn: "IdCompte",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Kiosques_KiosqueIdKiosque",
                        column: x => x.KiosqueIdKiosque,
                        principalTable: "Kiosques",
                        principalColumn: "IdKiosque");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agences_IdResponsable",
                table: "Agences",
                column: "IdResponsable");

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_IdClient",
                table: "Comptes",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Kiosques_IdAgence",
                table: "Kiosques",
                column: "IdAgence");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IdCompte",
                table: "Transactions",
                column: "IdCompte");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_KiosqueIdKiosque",
                table: "Transactions",
                column: "KiosqueIdKiosque");

            migrationBuilder.CreateIndex(
                name: "IX_Villes_IdPays",
                table: "Villes",
                column: "IdPays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrateurs");

            migrationBuilder.DropTable(
                name: "NotificationKiosques");

            migrationBuilder.DropTable(
                name: "NotificationsClients");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Villes");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Kiosques");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Agences");

            migrationBuilder.DropTable(
                name: "ResponsableAgences");
        }
    }
}
