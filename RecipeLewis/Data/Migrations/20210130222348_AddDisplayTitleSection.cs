using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeLewis.Data.Migrations
{
    public partial class AddDisplayTitleSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayTitle",
                table: "Section",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayTitle",
                table: "Section");
        }
    }
}
