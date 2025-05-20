using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlquileresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Localidad",
                table: "Propiedades",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Localidad",
                table: "Propiedades");
        }
    }
}
