using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateDatabase_V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_room_RoomId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_tour_TourId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_user_UserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_hotel_HotelId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_tour_TourId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_user_UserId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_schedule_tour_TourId",
                table: "schedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TourFavorites_tour_TourId",
                table: "TourFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_TourFavorites_user_UserId",
                table: "TourFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourFavorites",
                table: "TourFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_schedule",
                table: "schedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_evaluate",
                table: "evaluate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.RenameTable(
                name: "TourFavorites",
                newName: "tourfavorite");

            migrationBuilder.RenameTable(
                name: "schedule",
                newName: "tourschedule");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "payment");

            migrationBuilder.RenameTable(
                name: "evaluate",
                newName: "review");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "booking");

            migrationBuilder.RenameIndex(
                name: "IX_TourFavorites_TourId",
                table: "tourfavorite",
                newName: "IX_tourfavorite_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_schedule_TourId",
                table: "tourschedule",
                newName: "IX_tourschedule_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingId",
                table: "payment",
                newName: "IX_payment_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_evaluate_UserId",
                table: "review",
                newName: "IX_review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_evaluate_TourId",
                table: "review",
                newName: "IX_review_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_evaluate_HotelId",
                table: "review",
                newName: "IX_review_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_UserId",
                table: "booking",
                newName: "IX_booking_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_TourId",
                table: "booking",
                newName: "IX_booking_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_RoomId",
                table: "booking",
                newName: "IX_booking_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourfavorite",
                table: "tourfavorite",
                columns: new[] { "UserId", "TourId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_tourschedule",
                table: "tourschedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_payment",
                table: "payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_review",
                table: "review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_booking",
                table: "booking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_room_RoomId",
                table: "booking",
                column: "RoomId",
                principalTable: "room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_user_UserId",
                table: "booking",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payment_booking_BookingId",
                table: "payment",
                column: "BookingId",
                principalTable: "booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_review_hotel_HotelId",
                table: "review",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_review_tour_TourId",
                table: "review",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_review_user_UserId",
                table: "review",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourfavorite_tour_TourId",
                table: "tourfavorite",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourfavorite_user_UserId",
                table: "tourfavorite",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tourschedule_tour_TourId",
                table: "tourschedule",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_room_RoomId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_user_UserId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_payment_booking_BookingId",
                table: "payment");

            migrationBuilder.DropForeignKey(
                name: "FK_review_hotel_HotelId",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_review_tour_TourId",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_review_user_UserId",
                table: "review");

            migrationBuilder.DropForeignKey(
                name: "FK_tourfavorite_tour_TourId",
                table: "tourfavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_tourfavorite_user_UserId",
                table: "tourfavorite");

            migrationBuilder.DropForeignKey(
                name: "FK_tourschedule_tour_TourId",
                table: "tourschedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourschedule",
                table: "tourschedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tourfavorite",
                table: "tourfavorite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_review",
                table: "review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_payment",
                table: "payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_booking",
                table: "booking");

            migrationBuilder.RenameTable(
                name: "tourschedule",
                newName: "schedule");

            migrationBuilder.RenameTable(
                name: "tourfavorite",
                newName: "TourFavorites");

            migrationBuilder.RenameTable(
                name: "review",
                newName: "evaluate");

            migrationBuilder.RenameTable(
                name: "payment",
                newName: "Payments");

            migrationBuilder.RenameTable(
                name: "booking",
                newName: "Bookings");

            migrationBuilder.RenameIndex(
                name: "IX_tourschedule_TourId",
                table: "schedule",
                newName: "IX_schedule_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_tourfavorite_TourId",
                table: "TourFavorites",
                newName: "IX_TourFavorites_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_review_UserId",
                table: "evaluate",
                newName: "IX_evaluate_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_review_TourId",
                table: "evaluate",
                newName: "IX_evaluate_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_review_HotelId",
                table: "evaluate",
                newName: "IX_evaluate_HotelId");

            migrationBuilder.RenameIndex(
                name: "IX_payment_BookingId",
                table: "Payments",
                newName: "IX_Payments_BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_UserId",
                table: "Bookings",
                newName: "IX_Bookings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_TourId",
                table: "Bookings",
                newName: "IX_Bookings_TourId");

            migrationBuilder.RenameIndex(
                name: "IX_booking_RoomId",
                table: "Bookings",
                newName: "IX_Bookings_RoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_schedule",
                table: "schedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourFavorites",
                table: "TourFavorites",
                columns: new[] { "UserId", "TourId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_evaluate",
                table: "evaluate",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_room_RoomId",
                table: "Bookings",
                column: "RoomId",
                principalTable: "room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_tour_TourId",
                table: "Bookings",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_user_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_evaluate_hotel_HotelId",
                table: "evaluate",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_evaluate_tour_TourId",
                table: "evaluate",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_evaluate_user_UserId",
                table: "evaluate",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_schedule_tour_TourId",
                table: "schedule",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourFavorites_tour_TourId",
                table: "TourFavorites",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourFavorites_user_UserId",
                table: "TourFavorites",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
