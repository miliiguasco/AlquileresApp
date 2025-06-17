using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlquileresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMontoAPagarAndTipoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Propiedades_PropiedadId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_ClienteId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarjetas_Reservas_ReservaId",
                table: "Tarjetas");

            migrationBuilder.DropIndex(
                name: "IX_Tarjetas_ReservaId",
                table: "Tarjetas");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "Tarjetas");

            migrationBuilder.AddColumn<decimal>(
                name: "Saldo",
                table: "Tarjetas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "PropiedadId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoAPagar",
                table: "Reservas",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoPago",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoAPagar",
                table: "Propiedades",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MontoPagoAnticipado",
                table: "Propiedades",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TipoPago",
                table: "Propiedades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Propiedades_PropiedadId",
                table: "Reservas",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Propiedades_PropiedadId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_ClienteId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "Tarjetas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "MontoAPagar",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "TipoPago",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "MontoAPagar",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "MontoPagoAnticipado",
                table: "Propiedades");

            migrationBuilder.DropColumn(
                name: "TipoPago",
                table: "Propiedades");

            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "Tarjetas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PropiedadId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarjetas_ReservaId",
                table: "Tarjetas",
                column: "ReservaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Propiedades_PropiedadId",
                table: "Reservas",
                column: "PropiedadId",
                principalTable: "Propiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarjetas_Reservas_ReservaId",
                table: "Tarjetas",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
