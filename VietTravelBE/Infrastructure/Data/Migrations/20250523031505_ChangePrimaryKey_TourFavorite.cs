using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VietTravelBE.Infrastructure.Data.Migrations
{
    public partial class ChangePrimaryKey_TourFavorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "tourfavorite");

            migrationBuilder.CreateTable(
            name: "tourfavorite",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                UserId = table.Column<string>(nullable: false),
                TourId = table.Column<int>(nullable: false),
                CreatedAt = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tourfavorite", x => x.Id);

                table.ForeignKey(
                    name: "FK_tourfavorite_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);

                table.ForeignKey(
                    name: "FK_tourfavorite_tour_TourId",
                    column: x => x.TourId,
                    principalTable: "tour",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
            name: "IX_tourfavorite_UserId_TourId",
            table: "tourfavorite",
            columns: new[] { "UserId", "TourId" },
            unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tourfavorite_TourId",
                table: "tourfavorite",
                column: "TourId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "tourfavorite");
            migrationBuilder.CreateTable(
            name: "tourfavorite",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                TourId = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_tourfavorite", x => new { x.UserId, x.TourId });

                table.ForeignKey(
                name: "FK_tourfavorite_AspNetUsers_UserId",
                column: x => x.UserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

                table.ForeignKey(
                name: "FK_tourfavorite_tour_TourId",
                column: x => x.TourId,
                principalTable: "tour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            });

            migrationBuilder.CreateIndex(
            name: "IX_tourfavorite_TourId",
            table: "tourfavorite",
            column: "TourId");
        }
    }
}
