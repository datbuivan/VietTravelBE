using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class UpdateImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "hotel");

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "image",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_image_hotel_EntityId",
                table: "image",
                column: "EntityId",
                principalTable: "hotel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_image_tour_EntityId",
                table: "image",
                column: "EntityId",
                principalTable: "tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_hotel_EntityId",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_image_tour_EntityId",
                table: "image");

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "room");

            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "image");

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "tour",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "hotel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
