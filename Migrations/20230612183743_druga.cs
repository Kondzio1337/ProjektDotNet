using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class druga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connector_Produkt_ProduktId",
                table: "Connector");

            migrationBuilder.DropForeignKey(
                name: "FK_Connector_kategorie_KategoriaId",
                table: "Connector");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Connector",
                table: "Connector");

            migrationBuilder.RenameTable(
                name: "Connector",
                newName: "connectors");

            migrationBuilder.RenameIndex(
                name: "IX_Connector_ProduktId",
                table: "connectors",
                newName: "IX_connectors_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_Connector_KategoriaId",
                table: "connectors",
                newName: "IX_connectors_KategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_connectors",
                table: "connectors",
                column: "ConnectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_connectors_Produkt_ProduktId",
                table: "connectors",
                column: "ProduktId",
                principalTable: "Produkt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_connectors_kategorie_KategoriaId",
                table: "connectors",
                column: "KategoriaId",
                principalTable: "kategorie",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_connectors_Produkt_ProduktId",
                table: "connectors");

            migrationBuilder.DropForeignKey(
                name: "FK_connectors_kategorie_KategoriaId",
                table: "connectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_connectors",
                table: "connectors");

            migrationBuilder.RenameTable(
                name: "connectors",
                newName: "Connector");

            migrationBuilder.RenameIndex(
                name: "IX_connectors_ProduktId",
                table: "Connector",
                newName: "IX_Connector_ProduktId");

            migrationBuilder.RenameIndex(
                name: "IX_connectors_KategoriaId",
                table: "Connector",
                newName: "IX_Connector_KategoriaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Connector",
                table: "Connector",
                column: "ConnectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connector_Produkt_ProduktId",
                table: "Connector",
                column: "ProduktId",
                principalTable: "Produkt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Connector_kategorie_KategoriaId",
                table: "Connector",
                column: "KategoriaId",
                principalTable: "kategorie",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
