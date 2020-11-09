using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace tp1_restaurant.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Prenom = table.Column<string>(maxLength: 100, nullable: false),
                    Nom = table.Column<string>(maxLength: 100, nullable: false),
                    TypeReservation = table.Column<int>(nullable: false),
                    Courriel = table.Column<string>(nullable: false),
                    Datevisite = table.Column<DateTime>(nullable: false),
                    QualiteRepas = table.Column<int>(nullable: false),
                    QualiteService = table.Column<int>(nullable: false),
                    Commentaires = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                columns: table => new
                {
                    PromotionId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TypePromotion = table.Column<int>(nullable: false),
                    TauxApplicable = table.Column<int>(nullable: false),
                    DescriptionPromotion = table.Column<string>(maxLength: 300, nullable: false),
                    DateDebut = table.Column<DateTime>(nullable: false),
                    DateFin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.PromotionId);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Prenom = table.Column<string>(maxLength: 100, nullable: false),
                    Nom = table.Column<string>(maxLength: 100, nullable: false),
                    TypeReservation = table.Column<int>(nullable: false),
                    Courriel = table.Column<string>(nullable: false),
                    DateHeureReservation = table.Column<DateTime>(nullable: false),
                    NumeroTelephone = table.Column<string>(nullable: false),
                    NombrePersonnes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Promotions");

            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
