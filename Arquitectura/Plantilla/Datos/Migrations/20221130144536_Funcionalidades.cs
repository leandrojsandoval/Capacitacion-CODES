using System;
using ARQ.Datos.Utils;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class Funcionalidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_FUNCIONALIDADES",
                columns: table => new
                {
                    ID_FUNCIONALIDAD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_FUNCIONALIDADES", x => x.ID_FUNCIONALIDAD);
                    table.ForeignKey(
                        name: "FK_TBL_FUNCIONALIDADES_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_FUNCIONALIDADES_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TIPOS_ACCESO",
                columns: table => new
                {
                    ID_TIPO_ACCESO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TIPOS_ACCESO", x => x.ID_TIPO_ACCESO);
                    table.ForeignKey(
                        name: "FK_TBL_TIPOS_ACCESO_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_TIPOS_ACCESO_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_FUNCIONALIDADES_ROL",
                columns: table => new
                {
                    ID_FUNCIONALIDAD = table.Column<int>(type: "int", nullable: false),
                    ID_ROL = table.Column<int>(type: "int", nullable: false),
                    ID_TIPO_ACCESO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_FUNCIONALIDADES_ROL", x => new { x.ID_FUNCIONALIDAD, x.ID_ROL });
                    table.ForeignKey(
                        name: "FK_TBL_FUNCIONALIDADES_ROL_TBL_FUNCIONALIDADES_ID_FUNCIONALIDAD",
                        column: x => x.ID_FUNCIONALIDAD,
                        principalTable: "TBL_FUNCIONALIDADES",
                        principalColumn: "ID_FUNCIONALIDAD",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_FUNCIONALIDADES_ROL_TBL_TIPOS_ACCESO_ID_TIPO_ACCESO",
                        column: x => x.ID_TIPO_ACCESO,
                        principalTable: "TBL_TIPOS_ACCESO",
                        principalColumn: "ID_TIPO_ACCESO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_FUNCIONALIDADES_ID_USUARIO_ALTA",
                table: "TBL_FUNCIONALIDADES",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_FUNCIONALIDADES_ID_USUARIO_MODIFICACION",
                table: "TBL_FUNCIONALIDADES",
                column: "ID_USUARIO_MODIFICACION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_FUNCIONALIDADES_ROL_ID_TIPO_ACCESO",
                table: "TBL_FUNCIONALIDADES_ROL",
                column: "ID_TIPO_ACCESO");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TIPOS_ACCESO_ID_USUARIO_ALTA",
                table: "TBL_TIPOS_ACCESO",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_TIPOS_ACCESO_ID_USUARIO_MODIFICACION",
                table: "TBL_TIPOS_ACCESO",
                column: "ID_USUARIO_MODIFICACION");

            MigrationUtils.RunScripts(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_FUNCIONALIDADES_ROL");

            migrationBuilder.DropTable(
                name: "TBL_FUNCIONALIDADES");

            migrationBuilder.DropTable(
                name: "TBL_TIPOS_ACCESO");
        }
    }
}
