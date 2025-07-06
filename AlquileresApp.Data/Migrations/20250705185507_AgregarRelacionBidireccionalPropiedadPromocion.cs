using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlquileresApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionBidireccionalPropiedadPromocion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Promociones");

            migrationBuilder.CreateTable(
                name: "PromocionPropiedad",
                columns: table => new
                {
                    PromocionesId = table.Column<int>(type: "INTEGER", nullable: false),
                    PropiedadesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromocionPropiedad", x => new { x.PromocionesId, x.PropiedadesId });
                    table.ForeignKey(
                        name: "FK_PromocionPropiedad_Promociones_PromocionesId",
                        column: x => x.PromocionesId,
                        principalTable: "Promociones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromocionPropiedad_Propiedades_PropiedadesId",
                        column: x => x.PropiedadesId,
                        principalTable: "Propiedades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromocionPropiedad_PropiedadesId",
                table: "PromocionPropiedad",
                column: "PropiedadesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromocionPropiedad");

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Promociones",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
