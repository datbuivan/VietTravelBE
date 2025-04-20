using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class AddIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "user",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "user",
                newName: "RefreshTokenExpiryTime");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "user",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "user",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "user",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "user",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "user",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "user",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "user",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "user",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "user");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "user");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "user");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "user");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "user");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "user");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "user");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "user");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "user");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "user");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "user",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTime",
                table: "user",
                newName: "DateOfBirth");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "user",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "user",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "user",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "user",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "user",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "user",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
