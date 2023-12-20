using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class CreacionDeTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_PELICULAS",
                columns: table => new
                {
                    ID_PELICULA = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IMAGEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DESCRIPCION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PELICULAS", x => x.ID_PELICULA);
                    table.ForeignKey(
                        name: "FK_TBL_PELICULAS_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_PELICULAS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SUCURSALES",
                columns: table => new
                {
                    ID_SUCURSAL = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PRECIO = table.Column<double>(type: "float", nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SUCURSALES", x => x.ID_SUCURSAL);
                    table.ForeignKey(
                        name: "FK_TBL_SUCURSALES_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_SUCURSALES_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_HORARIOS",
                columns: table => new
                {
                    ID_HORARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_PELICULA = table.Column<int>(type: "int", nullable: false),
                    ID_SUCURSAL = table.Column<int>(type: "int", nullable: false),
                    HORA = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false),
                    FECHA_ALTA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_USUARIO_ALTA = table.Column<int>(type: "int", nullable: false),
                    FECHA_MODIFICACION = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ID_USUARIO_MODIFICACION = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_HORARIOS", x => x.ID_HORARIO);
                    table.ForeignKey(
                        name: "FK_TBL_HORARIOS_TBL_PELICULAS_ID_PELICULA",
                        column: x => x.ID_PELICULA,
                        principalTable: "TBL_PELICULAS",
                        principalColumn: "ID_PELICULA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_HORARIOS_TBL_SUCURSALES_ID_SUCURSAL",
                        column: x => x.ID_SUCURSAL,
                        principalTable: "TBL_SUCURSALES",
                        principalColumn: "ID_SUCURSAL",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_HORARIOS_TBL_USUARIOS_ID_USUARIO_ALTA",
                        column: x => x.ID_USUARIO_ALTA,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_HORARIOS_TBL_USUARIOS_ID_USUARIO_MODIFICACION",
                        column: x => x.ID_USUARIO_MODIFICACION,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ORDENES",
                columns: table => new
                {
                    ID_ORDEN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_HORARIO = table.Column<int>(type: "int", nullable: false),
                    CANTIDAD = table.Column<int>(type: "int", nullable: false),
                    TOTAL = table.Column<double>(type: "float", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ORDENES", x => x.ID_ORDEN);
                    table.ForeignKey(
                        name: "FK_TBL_ORDENES_TBL_HORARIOS_ID_HORARIO",
                        column: x => x.ID_HORARIO,
                        principalTable: "TBL_HORARIOS",
                        principalColumn: "ID_HORARIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_ORDENES_TBL_USUARIOS_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TBL_USUARIOS",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HORARIOS_ID_PELICULA",
                table: "TBL_HORARIOS",
                column: "ID_PELICULA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HORARIOS_ID_SUCURSAL",
                table: "TBL_HORARIOS",
                column: "ID_SUCURSAL");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HORARIOS_ID_USUARIO_ALTA",
                table: "TBL_HORARIOS",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_HORARIOS_ID_USUARIO_MODIFICACION",
                table: "TBL_HORARIOS",
                column: "ID_USUARIO_MODIFICACION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ORDENES_ID_HORARIO",
                table: "TBL_ORDENES",
                column: "ID_HORARIO");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ORDENES_UsuarioId",
                table: "TBL_ORDENES",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PELICULAS_ID_USUARIO_ALTA",
                table: "TBL_PELICULAS",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PELICULAS_ID_USUARIO_MODIFICACION",
                table: "TBL_PELICULAS",
                column: "ID_USUARIO_MODIFICACION");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUCURSALES_ID_USUARIO_ALTA",
                table: "TBL_SUCURSALES",
                column: "ID_USUARIO_ALTA");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUCURSALES_ID_USUARIO_MODIFICACION",
                table: "TBL_SUCURSALES",
                column: "ID_USUARIO_MODIFICACION");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ORDENES");

            migrationBuilder.DropTable(
                name: "TBL_HORARIOS");

            migrationBuilder.DropTable(
                name: "TBL_PELICULAS");

            migrationBuilder.DropTable(
                name: "TBL_SUCURSALES");
        }
    }
}
