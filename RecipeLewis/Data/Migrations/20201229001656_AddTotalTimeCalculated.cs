using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeLewis.Data.Migrations
{
    public partial class AddTotalTimeCalculated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TotalTimeCalculated",
                table: "Recipes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTimeCalculated",
                table: "Recipes");
        }
    }
}
