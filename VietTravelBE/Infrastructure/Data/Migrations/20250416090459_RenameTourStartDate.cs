using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class RenameTourStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_TourStartDates_TourStartDateId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_TourStartDates_tour_TourId",
                table: "TourStartDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourStartDates",
                table: "TourStartDates");

            migrationBuilder.RenameTable(
                name: "TourStartDates",
                newName: "tourstartdate");

            migrationBuilder.RenameIndex(
                name: "IX_TourStartDates_TourId",
                table: "tourstartdate",
                newName: "IX_tourstartdate_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourstartdate",
                table: "tourstartdate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_tourstartdate_TourStartDateId",
                table: "booking",
                column: "TourStartDateId",
                principalTable: "tourstartdate",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourstartdate_tour_TourId",
                table: "tourstartdate",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_tourstartdate_TourStartDateId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_tourstartdate_tour_TourId",
                table: "tourstartdate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourstartdate",
                table: "tourstartdate");

            migrationBuilder.RenameTable(
                name: "tourstartdate",
                newName: "TourStartDates");

            migrationBuilder.RenameIndex(
                name: "IX_tourstartdate_TourId",
                table: "TourStartDates",
                newName: "IX_TourStartDates_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourStartDates",
                table: "TourStartDates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_TourStartDates_TourStartDateId",
                table: "booking",
                column: "TourStartDateId",
                principalTable: "TourStartDates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourStartDates_tour_TourId",
                table: "TourStartDates",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");
        }
    }
}
