using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningsService.Migrations
{
    /// <inheritdoc />
    public partial class ImproveModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScreeningFeature");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesRaw",
                table: "Screenings",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeaturesRaw",
                table: "Screenings");

            migrationBuilder.CreateTable(
                name: "ScreeningFeature",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScreeningId = table.Column<Guid>(type: "uuid", nullable: false),
                    Feature = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningFeature_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningFeature_ScreeningId",
                table: "ScreeningFeature",
                column: "ScreeningId");
        }
    }
}
