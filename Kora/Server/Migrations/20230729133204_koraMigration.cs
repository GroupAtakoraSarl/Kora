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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
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
                    DeviseAgence = table.Column<string>(type: "TEXT", nullable: false),
                    SoldeInitial = table.Column<double>(type: "REAL", nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kiosques",
                columns: table => new
                {
                    IDKiosque = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    AdresseKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    ContactKiosque = table.Column<string>(type: "TEXT", nullable: false),
                    IdAgence = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kiosques", x => x.IDKiosque);
                    table.ForeignKey(
                        name: "FK_Kiosques_Agences_IdAgence",
                        column: x => x.IdAgence,
                        principalTable: "Agences",
                        principalColumn: "IdAgence",
                        onDelete: ReferentialAction.Restrict);
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
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "Kiosques");

            migrationBuilder.DropTable(
                name: "Villes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Agences");

            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "ResponsableAgences");
        }
    }
}
