using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class EditPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdultPrice",
                table: "tour",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceOneNight",
                table: "room",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "hotel",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "hotel");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "tour",
                newName: "AdultPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "room",
                newName: "PriceOneNight");
        }
    }
}
