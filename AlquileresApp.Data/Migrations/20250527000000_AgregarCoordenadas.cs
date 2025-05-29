using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlquileresApp.Data.Migrations
{
    public partial class AgregarCoordenadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitud",
                table: "Propiedades",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitud",
                table: "Propiedades",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Propiedades");
        }
    }
} 