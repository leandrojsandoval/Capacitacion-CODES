using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations {

    public partial class CreacionDeTablasProductosYVentas : Migration {
        
        protected override void Up (MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "TLB_VENTAS",
                columns: table => new {
                    ID_VENTA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FECHA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_TLB_VENTAS", x => x.ID_VENTA);
                    table.ForeignKey(
                        name: "FK_TLB_VENTAS_TBL_CLIENTES_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "TBL_CLIENTES",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_PRODUCTOS",
                columns: table => new {
                    ID_PRODUCTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PRECIO = table.Column<double>(type: "float", nullable: false),
                    ID_VENTA = table.Column<int>(type: "int", nullable: true),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
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
                    table.ForeignKey(
                        name: "FK_TBL_PRODUCTOS_TLB_VENTAS_ID_VENTA",
                        column: x => x.ID_VENTA,
                        principalTable: "TLB_VENTAS",
                        principalColumn: "ID_VENTA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PRODUCTOS_ID_USUARIO_ALTA",
                table: "TBL_PRODUCTOS",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PRODUCTOS_ID_USUARIO_MODIFICACION",
                table: "TBL_PRODUCTOS",
                column: "ID_USUARIO_MODIFICACION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PRODUCTOS_ID_VENTA",
                table: "TBL_PRODUCTOS",
                column: "ID_VENTA");

            migrationBuilder.CreateIndex(
                name: "IX_TLB_VENTAS_ID_CLIENTE",
                table: "TLB_VENTAS",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_TLB_VENTAS_ID_USUARIO_ALTA",
                table: "TLB_VENTAS",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TLB_VENTAS_ID_USUARIO_MODIFICACION",
                table: "TLB_VENTAS",
                column: "ID_USUARIO_MODIFICACION");
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "TBL_PRODUCTOS");

            migrationBuilder.DropTable(
                name: "TLB_VENTAS");
        }
    }

}
