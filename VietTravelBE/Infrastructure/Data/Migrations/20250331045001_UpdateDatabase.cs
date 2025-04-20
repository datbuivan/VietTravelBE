using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_city_CityId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_hotel_HotelId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_room_hotel_HotelId",
                table: "room");

            migrationBuilder.DropTable(
                name: "ScheduleTourPackage");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "tourpackage");

            migrationBuilder.DropTable(
                name: "timepackage");

            migrationBuilder.DropColumn(
                name: "UniCodeName",
                table: "user");

            migrationBuilder.DropColumn(
                name: "MediumStar",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "NumberOfEvaluate",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "PriceBase",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "PriceTourGuide",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "tour");

            migrationBuilder.RenameColumn(
                name: "Eva",
                table: "evaluate",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "evaluate",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "evaluate",
                newName: "TourId");

            migrationBuilder.RenameIndex(
                name: "IX_evaluate_CityId",
                table: "evaluate",
                newName: "IX_evaluate_TourId");

            migrationBuilder.AddColumn<decimal>(
                name: "AdultPrice",
                table: "tour",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ChildPrice",
                table: "tour",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "evaluate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "evaluate",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "evaluate",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Adults = table.Column<int>(type: "int", nullable: false),
                    Children = table.Column<int>(type: "int", nullable: false),
                    TotalPeople = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_tour_TourId",
                        column: x => x.TourId,
                        principalTable: "tour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HotelTour",
                columns: table => new
                {
                    HotelsId = table.Column<int>(type: "int", nullable: false),
                    ToursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelTour", x => new { x.HotelsId, x.ToursId });
                    table.ForeignKey(
                        name: "FK_HotelTour_hotel_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelTour_tour_ToursId",
                        column: x => x.ToursId,
                        principalTable: "tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourFavorites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFavorites", x => new { x.UserId, x.TourId });
                    table.ForeignKey(
                        name: "FK_TourFavorites_tour_TourId",
                        column: x => x.TourId,
                        principalTable: "tour",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TourFavorites_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tourguide",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourguide", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TourId",
                table: "Bookings",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelTour_ToursId",
                table: "HotelTour",
                column: "ToursId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TourFavorites_TourId",
                table: "TourFavorites",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourTourGuide_ToursId",
                table: "TourTourGuide",
                column: "ToursId");

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
                name: "FK_room_hotel_HotelId",
                table: "room",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_hotel_HotelId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_evaluate_tour_TourId",
                table: "evaluate");

            migrationBuilder.DropForeignKey(
                name: "FK_room_hotel_HotelId",
                table: "room");

            migrationBuilder.DropTable(
                name: "HotelTour");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "TourFavorites");

            migrationBuilder.DropTable(
                name: "TourTourGuide");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "tourguide");

            migrationBuilder.DropColumn(
                name: "AdultPrice",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "ChildPrice",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "evaluate");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "evaluate");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "evaluate",
                newName: "Eva");

            migrationBuilder.RenameColumn(
                name: "TourId",
                table: "evaluate",
                newName: "CityId");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "evaluate",
                newName: "Content");

            migrationBuilder.RenameIndex(
                name: "IX_evaluate_TourId",
                table: "evaluate",
                newName: "IX_evaluate_CityId");

            migrationBuilder.AddColumn<string>(
                name: "UniCodeName",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MediumStar",
                table: "tour",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEvaluate",
                table: "tour",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBase",
                table: "tour",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTourGuide",
                table: "tour",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "tour",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelId",
                table: "evaluate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduleTourPackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTourPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTourPackage_schedule_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "schedule",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "timepackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timepackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tourpackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    TimePackageId = table.Column<int>(type: "int", nullable: false),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ListScheduleTourPackage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfAdult = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tourpackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tourpackage_hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tourpackage_timepackage_TimePackageId",
                        column: x => x.TimePackageId,
                        principalTable: "timepackage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tourpackage_tour_TourId",
                        column: x => x.TourId,
                        principalTable: "tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TourPackageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ticket_tourpackage_TourPackageId",
                        column: x => x.TourPackageId,
                        principalTable: "tourpackage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ticket_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTourPackage_ScheduleId",
                table: "ScheduleTourPackage",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_TourPackageId",
                table: "ticket",
                column: "TourPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ticket_UserId",
                table: "ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tourpackage_HotelId",
                table: "tourpackage",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_tourpackage_TimePackageId",
                table: "tourpackage",
                column: "TimePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_tourpackage_TourId",
                table: "tourpackage",
                column: "TourId");

            migrationBuilder.AddForeignKey(
                name: "FK_evaluate_city_CityId",
                table: "evaluate",
                column: "CityId",
                principalTable: "city",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_evaluate_hotel_HotelId",
                table: "evaluate",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_room_hotel_HotelId",
                table: "room",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
