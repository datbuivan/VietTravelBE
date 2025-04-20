using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class CreateImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking");

            migrationBuilder.DropColumn(
                name: "PriceOneNight",
                table: "hotel");

            migrationBuilder.AlterColumn<string>(
                name: "TitleIntroduct",
                table: "tour",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AlterColumn<string>(
                name: "Pictures",
                table: "tour",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "city",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    ImageType = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "region",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_region", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_RegionId",
                table: "city",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_image_EntityId_ImageType",
                table: "image",
                columns: new[] { "EntityId", "ImageType" });

            migrationBuilder.AddForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_city_region_RegionId",
                table: "city",
                column: "RegionId",
                principalTable: "region",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking");

            migrationBuilder.DropForeignKey(
                name: "FK_city_region_RegionId",
                table: "city");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "region");

            migrationBuilder.DropIndex(
                name: "IX_city_RegionId",
                table: "city");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "city");

            migrationBuilder.AlterColumn<string>(
                name: "TitleIntroduct",
                table: "tour",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pictures",
                table: "tour",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceOneNight",
                table: "hotel",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_booking_hotel_HotelId",
                table: "booking",
                column: "HotelId",
                principalTable: "hotel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_booking_tour_TourId",
                table: "booking",
                column: "TourId",
                principalTable: "tour",
                principalColumn: "Id");
        }
    }
}
