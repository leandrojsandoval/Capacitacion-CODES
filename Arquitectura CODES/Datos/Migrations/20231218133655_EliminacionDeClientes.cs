using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class EliminacionDeClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CLIENTES");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CLIENTES",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    APELLIDO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FECHA_NACIMIENTO = table.Column<DateTime>(type: "Date", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CLIENTES", x => x.ID_CLIENTE);
                    table.ForeignKey(
                        name: "FK_TBL_CLIENTES_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_CLIENTES_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CLIENTES_ID_USUARIO_ALTA",
                table: "TBL_CLIENTES",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CLIENTES_ID_USUARIO_MODIFICACION",
                table: "TBL_CLIENTES",
                column: "ID_USUARIO_MODIFICACION");
        }
    }
}
