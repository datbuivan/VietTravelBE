using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateDatabase_V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_RoomId",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "booking",
                newName: "HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_RoomId",
                table: "booking",
                newName: "IX_booking_HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking");

            migrationBuilder.RenameColumn(
                name: "HotelId",
                table: "booking",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_HotelId",
                table: "booking",
                newName: "IX_booking_RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_RoomId",
                table: "booking",
                column: "RoomId",
                principalTable: "room",
                principalColumn: "Id");
        }
    }
}
