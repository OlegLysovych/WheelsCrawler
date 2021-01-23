using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsCrawler.Data.Migrations
{
    public partial class carmodelextending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CarUri",
                table: "Cars",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineСapacity",
                table: "Cars",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kilometrage",
                table: "Cars",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarUri",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "EngineСapacity",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Kilometrage",
                table: "Cars");
        }
    }
}
