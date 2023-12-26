using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ARQ.Datos.Migrations {
    public partial class CreateInitial : Migration {

        protected override void Up (MigrationBuilder migrationBuilder) {

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOGIN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NOMBRE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DIR_CORREO = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    ID_TURNO_TRABAJO = table.Column<int>(type: "int", nullable: true),
                    ID_USUARIO_SGAA = table.Column<int>(type: "int", nullable: false),
                    ACTIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.ID_USUARIO);
                });

        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");
        }
    }
}
