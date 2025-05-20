using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class RemoveFkImageTourHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_image_hotel_EntityId",
                table: "image");

            migrationBuilder.DropForeignKey(
                name: "FK_image_tour_EntityId",
                table: "image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
