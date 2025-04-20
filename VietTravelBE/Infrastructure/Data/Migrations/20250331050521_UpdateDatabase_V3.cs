using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateDatabase_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourTourGuide");

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "tourguide",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourGuideId",
                table: "tour",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tour_TourGuideId",
                table: "tour",
                column: "TourGuideId");

            migrationBuilder.AddForeignKey(
                name: "FK_tour_tourguide_TourGuideId",
                table: "tour",
                column: "TourGuideId",
                principalTable: "tourguide",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tour_tourguide_TourGuideId",
                table: "tour");

            migrationBuilder.DropIndex(
                name: "IX_tour_TourGuideId",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "tourguide");

            migrationBuilder.DropColumn(
                name: "TourGuideId",
                table: "tour");

            migrationBuilder.CreateTable(
                name: "TourTourGuide",
                columns: table => new
                {
                    GuidesId = table.Column<int>(type: "int", nullable: false),
                    ToursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourTourGuide", x => new { x.GuidesId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_TourTourGuide_tour_ToursId",
                        column: x => x.ToursId,
                        principalTable: "tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourTourGuide_tourguide_GuidesId",
                        column: x => x.GuidesId,
                        principalTable: "tourguide",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourTourGuide_ToursId",
                table: "TourTourGuide",
                column: "ToursId");
        }
    }
}
