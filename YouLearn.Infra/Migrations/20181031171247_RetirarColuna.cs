using Microsoft.EntityFrameworkCore.Migrations;

namespace YouLearn.Infra.Migrations
{
    public partial class RetirarColuna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teste",
                table: "Canal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teste",
                table: "Canal",
                nullable: true);
        }
    }
}
