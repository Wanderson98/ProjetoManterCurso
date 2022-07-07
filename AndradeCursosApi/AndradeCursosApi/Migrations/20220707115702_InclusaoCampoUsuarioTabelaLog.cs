using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AndradeCursosApi.Migrations
{
    public partial class InclusaoCampoUsuarioTabelaLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Usuario",
                table: "Logs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Usuario",
                table: "Logs");
        }
    }
}
