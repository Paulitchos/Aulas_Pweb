using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PWEB_AulasP_2223.Migrations
{
    public partial class Agendamento_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "Agendamentos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamentos_ApplicationUserID",
                table: "Agendamentos",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_AspNetUsers_ApplicationUserID",
                table: "Agendamentos",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_AspNetUsers_ApplicationUserID",
                table: "Agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_Agendamentos_ApplicationUserID",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Agendamentos");

            migrationBuilder.AddColumn<string>(
                name: "Cliente",
                table: "Agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
