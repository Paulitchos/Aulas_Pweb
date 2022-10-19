using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aula_P1.Data.Migrations
{
    public partial class Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos");

            migrationBuilder.RenameTable(
                name: "Cursos",
                newName: "Curso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Curso",
                table: "Curso",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Curso",
                table: "Curso");

            migrationBuilder.RenameTable(
                name: "Curso",
                newName: "Cursos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cursos",
                table: "Cursos",
                column: "Id");
        }
    }
}
