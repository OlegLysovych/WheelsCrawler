using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsCrawler.Data.Migrations
{
    public partial class ReferenceUserToCarWithURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RelatedQueryUrlId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RelatedQueryUrlId",
                table: "Cars",
                column: "RelatedQueryUrlId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Urls_RelatedQueryUrlId",
                table: "Cars",
                column: "RelatedQueryUrlId",
                principalTable: "Urls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Urls_RelatedQueryUrlId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RelatedQueryUrlId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RelatedQueryUrlId",
                table: "Cars");
        }
    }
}
