using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class AddTourStartDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tour_tourguide_TourGuideId",
                table: "tour");

            migrationBuilder.DropTable(
                name: "tourguide");

            migrationBuilder.DropIndex(
                name: "IX_tour_TourGuideId",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "TourGuideId",
                table: "tour");

            migrationBuilder.AddColumn<DateTime>(
                name: "HotelCheckInDate",
                table: "booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HotelCheckOutDate",
                table: "booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TourStartDateId",
                table: "booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TourStartDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvailableSlots = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourStartDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourStartDates_tour_TourId",
                        column: x => x.TourId,
                        principalTable: "tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_booking_TourStartDateId",
                table: "booking",
                column: "TourStartDateId");

            migrationBuilder.CreateIndex(
                name: "IX_TourStartDates_TourId",
                table: "TourStartDates",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_TourStartDates_TourStartDateId",
                table: "booking",
                column: "TourStartDateId",
                principalTable: "TourStartDates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_TourStartDates_TourStartDateId",
                table: "booking");

            migrationBuilder.DropTable(
                name: "TourStartDates");

            migrationBuilder.DropIndex(
                name: "IX_booking_TourStartDateId",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "HotelCheckInDate",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "HotelCheckOutDate",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "TourStartDateId",
                table: "booking");

            migrationBuilder.AddColumn<int>(
                name: "TourGuideId",
                table: "tour",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tourguide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourguide", x => x.Id);
                });

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
    }
}
