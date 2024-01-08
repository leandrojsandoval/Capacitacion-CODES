using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ARQ.Datos.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOGIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DIR_CORREO = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ID_TURNO_TRABAJO = table.Column<int>(type: "int", nullable: true),
                    ID_USUARIO_SGAA = table.Column<int>(type: "int", nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "TBL_MATERIALES",
                columns: table => new
                {
                    ID_MATERIAL_RODILLO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MULTIPLICADOR_TONELADAS = table.Column<double>(type: "float", nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_MATERIALES", x => x.ID_MATERIAL_RODILLO);
                    table.ForeignKey(
                        name: "FK_TBL_MATERIALES_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_MATERIALES_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MATERIALES_ID_USUARIO_ALTA",
                table: "TBL_MATERIALES",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MATERIALES_ID_USUARIO_MODIFICACION",
                table: "TBL_MATERIALES",
                column: "ID_USUARIO_MODIFICACION");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_MATERIALES");

            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");
        }
    }
}
