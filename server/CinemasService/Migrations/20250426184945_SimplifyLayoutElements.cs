using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemasService.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyLayoutElements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LayoutElements_PositionX",
                table: "LayoutElements");

            migrationBuilder.DropColumn(
                name: "PositionX",
                table: "LayoutElements");

            migrationBuilder.RenameColumn(
                name: "PositionY",
                table: "LayoutElements",
                newName: "Index");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutElements_PositionY",
                table: "LayoutElements",
                newName: "IX_LayoutElements_Index");

            migrationBuilder.AddColumn<int>(
                name: "Columns",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rows",
                table: "Layouts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Columns",
                table: "Layouts");

            migrationBuilder.DropColumn(
                name: "Rows",
                table: "Layouts");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "LayoutElements",
                newName: "PositionY");

            migrationBuilder.RenameIndex(
                name: "IX_LayoutElements_Index",
                table: "LayoutElements",
                newName: "IX_LayoutElements_PositionY");

            migrationBuilder.AddColumn<int>(
                name: "PositionX",
                table: "LayoutElements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LayoutElements_PositionX",
                table: "LayoutElements",
                column: "PositionX");
        }
    }
}
