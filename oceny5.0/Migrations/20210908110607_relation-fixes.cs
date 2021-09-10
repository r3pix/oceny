using Microsoft.EntityFrameworkCore.Migrations;

namespace oceny5._0.Migrations
{
    public partial class relationfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "Wykladowcy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Wykladowcy",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Studenci",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HashedPassword",
                table: "Studenci",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Wykladowcy");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "Wykladowcy");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Studenci");

            migrationBuilder.DropColumn(
                name: "HashedPassword",
                table: "Studenci");
        }
    }
}
