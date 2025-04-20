using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class EditTourSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "Pictures",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "PriceTicketAdult",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "PriceTicketKid",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "TicketEnable",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "ContentIntroduct",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tour");

            migrationBuilder.DropColumn(
                name: "TitleIntroduct",
                table: "tour");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tourschedule",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "tourschedule",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tourschedule",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "tourschedule");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "tourschedule");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "tourschedule",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "tourschedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pictures",
                table: "tourschedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTicketAdult",
                table: "tourschedule",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTicketKid",
                table: "tourschedule",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TicketEnable",
                table: "tourschedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "tour",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentIntroduct",
                table: "tour",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "tour",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleIntroduct",
                table: "tour",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }
    }
}
