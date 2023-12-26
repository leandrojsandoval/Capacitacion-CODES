using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class RenombreDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PRODUCTOS_TLB_VENTAS_ID_VENTA",
                table: "TBL_PRODUCTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_TLB_VENTAS_TBL_CLIENTES_ID_CLIENTE",
                table: "TLB_VENTAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                table: "TLB_VENTAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                table: "TLB_VENTAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TLB_VENTAS",
                table: "TLB_VENTAS");

            migrationBuilder.RenameTable(
                name: "TLB_VENTAS",
                newName: "TBL_VENTAS");

            migrationBuilder.RenameIndex(
                name: "IX_TLB_VENTAS_ID_USUARIO_MODIFICACION",
                table: "TBL_VENTAS",
                newName: "IX_TBL_VENTAS_ID_USUARIO_MODIFICACION");

            migrationBuilder.RenameIndex(
                name: "IX_TLB_VENTAS_ID_USUARIO_ALTA",
                table: "TBL_VENTAS",
                newName: "IX_TBL_VENTAS_ID_USUARIO_ALTA");

            migrationBuilder.RenameIndex(
                name: "IX_TLB_VENTAS_ID_CLIENTE",
                table: "TBL_VENTAS",
                newName: "IX_TBL_VENTAS_ID_CLIENTE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_VENTAS",
                table: "TBL_VENTAS",
                column: "ID_VENTA");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PRODUCTOS_TBL_VENTAS_ID_VENTA",
                table: "TBL_PRODUCTOS",
                column: "ID_VENTA",
                principalTable: "TBL_VENTAS",
                principalColumn: "ID_VENTA",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_VENTAS_TBL_CLIENTES_ID_CLIENTE",
                table: "TBL_VENTAS",
                column: "ID_CLIENTE",
                principalTable: "TBL_CLIENTES",
                principalColumn: "ID_CLIENTE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_VENTAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                table: "TBL_VENTAS",
                column: "ID_USUARIO_ALTA",
                principalTable: "TBL_USUARIOS",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_VENTAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                table: "TBL_VENTAS",
                column: "ID_USUARIO_MODIFICACION",
                principalTable: "TBL_USUARIOS",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PRODUCTOS_TBL_VENTAS_ID_VENTA",
                table: "TBL_PRODUCTOS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_VENTAS_TBL_CLIENTES_ID_CLIENTE",
                table: "TBL_VENTAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_VENTAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                table: "TBL_VENTAS");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_VENTAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                table: "TBL_VENTAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_VENTAS",
                table: "TBL_VENTAS");

            migrationBuilder.RenameTable(
                name: "TBL_VENTAS",
                newName: "TLB_VENTAS");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_VENTAS_ID_USUARIO_MODIFICACION",
                table: "TLB_VENTAS",
                newName: "IX_TLB_VENTAS_ID_USUARIO_MODIFICACION");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_VENTAS_ID_USUARIO_ALTA",
                table: "TLB_VENTAS",
                newName: "IX_TLB_VENTAS_ID_USUARIO_ALTA");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_VENTAS_ID_CLIENTE",
                table: "TLB_VENTAS",
                newName: "IX_TLB_VENTAS_ID_CLIENTE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TLB_VENTAS",
                table: "TLB_VENTAS",
                column: "ID_VENTA");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PRODUCTOS_TLB_VENTAS_ID_VENTA",
                table: "TBL_PRODUCTOS",
                column: "ID_VENTA",
                principalTable: "TLB_VENTAS",
                principalColumn: "ID_VENTA",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLB_VENTAS_TBL_CLIENTES_ID_CLIENTE",
                table: "TLB_VENTAS",
                column: "ID_CLIENTE",
                principalTable: "TBL_CLIENTES",
                principalColumn: "ID_CLIENTE",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                table: "TLB_VENTAS",
                column: "ID_USUARIO_ALTA",
                principalTable: "TBL_USUARIOS",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TLB_VENTAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                table: "TLB_VENTAS",
                column: "ID_USUARIO_MODIFICACION",
                principalTable: "TBL_USUARIOS",
                principalColumn: "ID_USUARIO",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
