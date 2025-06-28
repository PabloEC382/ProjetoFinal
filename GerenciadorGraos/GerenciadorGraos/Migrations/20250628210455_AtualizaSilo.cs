using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciadorGraos.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaSilo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Silos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    CapacidadeMaxima = table.Column<double>(type: "REAL", nullable: false),
                    CapacidadeAtual = table.Column<double>(type: "REAL", nullable: true),
                    Temperatura = table.Column<double>(type: "REAL", nullable: false),
                    TipoGrao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Silos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Silos");
        }
    }
}
