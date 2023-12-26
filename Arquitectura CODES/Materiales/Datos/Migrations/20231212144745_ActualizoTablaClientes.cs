using Microsoft.EntityFrameworkCore.Migrations;

namespace ARQ.Datos.Migrations
{
    public partial class ActualizoTablaClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_CLEINTE",
                table: "TBL_CLIENTES",
                newName: "ID_CLIENTE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_CLIENTE",
                table: "TBL_CLIENTES",
                newName: "ID_CLEINTE");
        }
    }
}
