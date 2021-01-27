using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RecipeLewis.Data.Migrations
{
    public partial class AddSections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "Step",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "Ingredient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    SectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.SectionID);
                    table.ForeignKey(
                        name: "FK_Section_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Step_SectionID",
                table: "Step",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_SectionID",
                table: "Ingredient",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Section_RecipeID",
                table: "Section",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Section_SectionID",
                table: "Ingredient",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Step_Section_SectionID",
                table: "Step",
                column: "SectionID",
                principalTable: "Section",
                principalColumn: "SectionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Section_SectionID",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Step_Section_SectionID",
                table: "Step");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Step_SectionID",
                table: "Step");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_SectionID",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Step");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Ingredient");
        }
    }
}