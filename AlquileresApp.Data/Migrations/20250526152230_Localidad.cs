using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlquileresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Localidad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_AdministradorId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_EncargadoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_AdministradorId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_EncargadoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "AdministradorId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "EncargadoId",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Usuarios",
                newName: "Contraseña");

            migrationBuilder.AddColumn<int>(
                name: "EncargadoId",
                table: "Propiedades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PagoAnticipado",
                table: "Propiedades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncargadoId",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "PagoAnticipado",
                table: "Propiedades");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "Password");

            migrationBuilder.AddColumn<int>(
                name: "AdministradorId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EncargadoId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_AdministradorId",
                table: "Reservas",
                column: "AdministradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_EncargadoId",
                table: "Reservas",
                column: "EncargadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_AdministradorId",
                table: "Reservas",
                column: "AdministradorId",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_EncargadoId",
                table: "Reservas",
                column: "EncargadoId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
