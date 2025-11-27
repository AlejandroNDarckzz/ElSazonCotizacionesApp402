using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElSazonCotizacionesApp.Migrations
{
    /// <inheritdoc />
    public partial class CarritoUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaAgregado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarritoUsuarios_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarritoUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarritoUsuarios_ProductoId",
                table: "CarritoUsuarios",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarritoUsuarios_UsuarioId",
                table: "CarritoUsuarios",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarritoUsuarios");
        }
    }
}
