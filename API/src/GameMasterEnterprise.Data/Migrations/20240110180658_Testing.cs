using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameMasterEnterprise.Data.Migrations
{
    public partial class Testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Situacao",
                table: "Sessao",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Valor",
                table: "Sessao",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Situacao",
                table: "Sessao");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Sessao");
        }
    }
}
