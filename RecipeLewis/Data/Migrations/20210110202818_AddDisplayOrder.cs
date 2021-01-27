using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeLewis.Data.Migrations
{
    public partial class AddDisplayOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Step",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Ingredient");
        }
    }
}