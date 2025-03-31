using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Pictures = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TitleIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ContentIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "timepackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timepackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UniCodeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PriceOneNight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    TitleIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ContentIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Pictures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hotel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_hotel_city_CityId",
                        column: x => x.CityId,
                        principalTable: "city",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Pictures = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    TitleIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ContentIntroduct = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    NumberOfEvaluate = table.Column<int>(type: "int", nullable: true),
                    MediumStar = table.Column<float>(type: "real", nullable: true),
                    PriceTourGuide = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PriceBase = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tour_city_CityId",
                        column: x => x.CityId,
                        principalTable: "city",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "evaluate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Eva = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_evaluate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_evaluate_city_CityId",
                        column: x => x.CityId,
                        principalTable: "city",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_evaluate_hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_evaluate_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PriceOneNight = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Superficies = table.Column<int>(type: "int", nullable: true),
                    TypeRoom = table.Column<int>(type: "int", nullable: false),
                    FreeWifi = table.Column<bool>(type: "bit", nullable: false),
                    BreakFast = table.Column<bool>(type: "bit", nullable: false),
                    FreeBreakFast = table.Column<bool>(type: "bit", nullable: false),
                    DrinkWater = table.Column<bool>(type: "bit", nullable: false),
                    CoffeeAndTea = table.Column<bool>(type: "bit", nullable: false),
                    Park = table.Column<bool>(type: "bit", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_room_hotel_HotelId",
                        column: x => x.HotelId,
                        principalTable: "hotel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "schedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketEnable = table.Column<int>(type: "int", nullable: true),
                    PriceTicketKid = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PriceTicketAdult = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Pictures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_schedule_tour_TourId",
                        column: x => x.TourId,
                        principalTable: "tour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tourpackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfAdult = table.Column<int>(type: "int", nullable: false),
                    NumberOfChildren = table.Column<int>(type: "int", nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ListScheduleTourPackage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    TimePackageId = table.Column<int>(type: "int", nullable: false)
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
                name: "ScheduleTourPackage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
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
                name: "ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TourPackageId = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_evaluate_CityId",
                table: "evaluate",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_evaluate_HotelId",
                table: "evaluate",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_evaluate_UserId",
                table: "evaluate",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_hotel_CityId",
                table: "hotel",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_room_HotelId",
                table: "room",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_schedule_TourId",
                table: "schedule",
                column: "TourId");

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
                name: "IX_tour_CityId",
                table: "tour",
                column: "CityId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "evaluate");

            migrationBuilder.DropTable(
                name: "room");

            migrationBuilder.DropTable(
                name: "ScheduleTourPackage");

            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "schedule");

            migrationBuilder.DropTable(
                name: "tourpackage");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "hotel");

            migrationBuilder.DropTable(
                name: "timepackage");

            migrationBuilder.DropTable(
                name: "tour");

            migrationBuilder.DropTable(
                name: "city");
        }
    }
}
