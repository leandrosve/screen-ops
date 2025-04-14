using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreeningsService.Migrations
{
    /// <inheritdoc />
    public partial class ScreeningSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ScreeningScheduleId",
                table: "Screenings",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ScreeningSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    CinemaId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FeaturesRaw = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DaysOfWeekRaw = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningSchedules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreeningScheduleTime",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    End = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreeningScheduleTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScreeningScheduleTime_ScreeningSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScreeningSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Screenings_ScreeningScheduleId",
                table: "Screenings",
                column: "ScreeningScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningSchedules_CinemaId",
                table: "ScreeningSchedules",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningSchedules_MovieId",
                table: "ScreeningSchedules",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningSchedules_RoomId",
                table: "ScreeningSchedules",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningSchedules_Status",
                table: "ScreeningSchedules",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ScreeningScheduleTime_ScheduleId",
                table: "ScreeningScheduleTime",
                column: "ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Screenings_ScreeningSchedules_ScreeningScheduleId",
                table: "Screenings",
                column: "ScreeningScheduleId",
                principalTable: "ScreeningSchedules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Screenings_ScreeningSchedules_ScreeningScheduleId",
                table: "Screenings");

            migrationBuilder.DropTable(
                name: "ScreeningScheduleTime");

            migrationBuilder.DropTable(
                name: "ScreeningSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Screenings_ScreeningScheduleId",
                table: "Screenings");

            migrationBuilder.DropColumn(
                name: "ScreeningScheduleId",
                table: "Screenings");
        }
    }
}
