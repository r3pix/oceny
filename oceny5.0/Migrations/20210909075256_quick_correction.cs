using Microsoft.EntityFrameworkCore.Migrations;

namespace oceny5._0.Migrations
{
    public partial class quick_correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Wykladowcy",
                newName: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Wykladowcy",
                newName: "Login");
        }
    }
}
