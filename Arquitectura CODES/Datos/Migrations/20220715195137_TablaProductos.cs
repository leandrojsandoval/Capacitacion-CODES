using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class TablaProductos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_PRODUCTO",
                table: "TBL_MATERIALES",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TBL_PRODUCTOS",
                columns: table => new
                {
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PRODUCTOS", x => x.ID_PRODUCTO);
                    table.ForeignKey(
                        name: "FK_TBL_PRODUCTOS_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_PRODUCTOS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MATERIALES_ID_PRODUCTO",
                table: "TBL_MATERIALES",
                column: "ID_PRODUCTO");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PRODUCTOS_ID_USUARIO_ALTA",
                table: "TBL_PRODUCTOS",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PRODUCTOS_ID_USUARIO_MODIFICACION",
                table: "TBL_PRODUCTOS",
                column: "ID_USUARIO_MODIFICACION");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_MATERIALES_TBL_PRODUCTOS_ID_PRODUCTO",
                table: "TBL_MATERIALES",
                column: "ID_PRODUCTO",
                principalTable: "TBL_PRODUCTOS",
                principalColumn: "ID_PRODUCTO",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_MATERIALES_TBL_PRODUCTOS_ID_PRODUCTO",
                table: "TBL_MATERIALES");

            migrationBuilder.DropTable(
                name: "TBL_PRODUCTOS");

            migrationBuilder.DropIndex(
                name: "IX_TBL_MATERIALES_ID_PRODUCTO",
                table: "TBL_MATERIALES");

            migrationBuilder.DropColumn(
                name: "ID_PRODUCTO",
                table: "TBL_MATERIALES");
        }
    }
}
