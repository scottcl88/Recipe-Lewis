using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeLewis.Data.Migrations
{
    public partial class NumberOfServings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumberOfServings",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfServings",
                table: "Recipes");
        }
    }
}