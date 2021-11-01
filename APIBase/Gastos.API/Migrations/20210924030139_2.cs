using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gastos.API.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaId = table.Column<int>(type: "int", nullable: true),
                    Grupo = table.Column<int>(type: "int", nullable: false),
                    Factor = table.Column<int>(type: "int", nullable: false),
                    FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UltimaModificacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donantes_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donantes_PersonaId",
                table: "Donantes",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donantes");
        }
    }
}
