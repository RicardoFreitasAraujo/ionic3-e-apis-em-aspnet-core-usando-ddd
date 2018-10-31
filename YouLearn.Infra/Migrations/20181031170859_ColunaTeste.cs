using Microsoft.EntityFrameworkCore.Migrations;

namespace YouLearn.Infra.Migrations
{
    public partial class ColunaTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teste",
                table: "Canal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teste",
                table: "Canal");
        }
    }
}
