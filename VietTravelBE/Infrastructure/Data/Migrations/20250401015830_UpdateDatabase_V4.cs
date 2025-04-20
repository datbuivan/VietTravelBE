using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateDatabase_V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelTour_hotel_HotelsId",
                table: "HotelTour");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelTour_tour_ToursId",
                table: "HotelTour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelTour",
                table: "HotelTour");

            migrationBuilder.RenameTable(
                name: "HotelTour",
                newName: "hoteltour");

            migrationBuilder.RenameColumn(
                name: "ToursId",
                table: "hoteltour",
                newName: "TourId");

            migrationBuilder.RenameColumn(
                name: "HotelsId",
                table: "hoteltour",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_HotelTour_ToursId",
                table: "hoteltour",
                newName: "IX_hoteltour_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hoteltour",
                table: "hoteltour",
                columns: new[] { "HotelId", "TourId" });

            migrationBuilder.AddForeignKey(
                name: "FK_hoteltour_hotel_HotelId",
                table: "hoteltour",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hoteltour_tour_TourId",
                table: "hoteltour",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hoteltour_hotel_HotelId",
                table: "hoteltour");

            migrationBuilder.DropForeignKey(
                name: "FK_hoteltour_tour_TourId",
                table: "hoteltour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hoteltour",
                table: "hoteltour");

            migrationBuilder.RenameTable(
                name: "hoteltour",
                newName: "HotelTour");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "HotelTour",
                newName: "ToursId");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "HotelTour",
                newName: "HotelsId");

            migrationBuilder.RenameIndex(
                name: "IX_hoteltour_TourId",
                table: "HotelTour",
                newName: "IX_HotelTour_ToursId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelTour",
                table: "HotelTour",
                columns: new[] { "HotelsId", "ToursId" });

            migrationBuilder.AddForeignKey(
                name: "FK_HotelTour_hotel_HotelsId",
                table: "HotelTour",
                column: "HotelsId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelTour_tour_ToursId",
                table: "HotelTour",
                column: "ToursId",
                principalTable: "tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
