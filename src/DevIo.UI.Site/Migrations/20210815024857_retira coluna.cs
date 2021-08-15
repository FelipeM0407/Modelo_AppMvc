using Microsoft.EntityFrameworkCore.Migrations;

namespace DevIO.UI.Site.Migrations
{
    public partial class retiracoluna : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Teste",
                table: "Alunos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Teste",
                table: "Alunos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
