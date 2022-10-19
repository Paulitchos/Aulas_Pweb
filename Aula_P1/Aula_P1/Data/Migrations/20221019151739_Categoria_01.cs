using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aula_P1.Data.Migrations
{
    public partial class Categoria_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");

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
    }
}
