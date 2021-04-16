using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsCrawler.Data.Migrations
{
    public partial class ExtendModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GearBox",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "CarTypes",
                newName: "WheelsName");

            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "CarBrands",
                newName: "WheelsName");

            migrationBuilder.AddColumn<string>(
                name: "RiaName",
                table: "CarTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RstName",
                table: "CarTypes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PublishDate",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Kilometrage",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "EngineСapacity",
                table: "Cars",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarTypeId",
                table: "Cars",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "CarBrandId",
                table: "Cars",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "CarFuelId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarGearboxId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarModelId",
                table: "Cars",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiaName",
                table: "CarBrands",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RstName",
                table: "CarBrands",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarFuels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WheelsName = table.Column<string>(type: "TEXT", nullable: true),
                    RiaName = table.Column<string>(type: "TEXT", nullable: true),
                    RstName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarFuels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarGearboxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WheelsName = table.Column<string>(type: "TEXT", nullable: true),
                    RiaName = table.Column<string>(type: "TEXT", nullable: true),
                    RstName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarGearboxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WheelsName = table.Column<string>(type: "TEXT", nullable: true),
                    RiaName = table.Column<string>(type: "TEXT", nullable: true),
                    RstName = table.Column<string>(type: "TEXT", nullable: true),
                    CarBrandId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModels_CarBrands_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars",
                column: "CarBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarFuelId",
                table: "Cars",
                column: "CarFuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarGearboxId",
                table: "Cars",
                column: "CarGearboxId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarModelId",
                table: "Cars",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars",
                column: "CarTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModels_CarBrandId",
                table: "CarModels",
                column: "CarBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarBrands_CarBrandId",
                table: "Cars",
                column: "CarBrandId",
                principalTable: "CarBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarFuels_CarFuelId",
                table: "Cars",
                column: "CarFuelId",
                principalTable: "CarFuels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarGearboxes_CarGearboxId",
                table: "Cars",
                column: "CarGearboxId",
                principalTable: "CarGearboxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarTypes_CarTypeId",
                table: "Cars",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarBrands_CarBrandId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarFuels_CarFuelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarGearboxes_CarGearboxId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModels_CarModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarTypes_CarTypeId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarFuels");

            migrationBuilder.DropTable(
                name: "CarGearboxes");

            migrationBuilder.DropTable(
                name: "CarModels");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarBrandId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarFuelId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarGearboxId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarModelId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarTypeId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RiaName",
                table: "CarTypes");

            migrationBuilder.DropColumn(
                name: "RstName",
                table: "CarTypes");

            migrationBuilder.DropColumn(
                name: "CarFuelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarGearboxId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarModelId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "RiaName",
                table: "CarBrands");

            migrationBuilder.DropColumn(
                name: "RstName",
                table: "CarBrands");

            migrationBuilder.RenameColumn(
                name: "WheelsName",
                table: "CarTypes",
                newName: "TypeName");

            migrationBuilder.RenameColumn(
                name: "WheelsName",
                table: "CarBrands",
                newName: "BrandName");

            migrationBuilder.AlterColumn<string>(
                name: "PublishDate",
                table: "Cars",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Cars",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Kilometrage",
                table: "Cars",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "EngineСapacity",
                table: "Cars",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "CarTypeId",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarBrandId",
                table: "Cars",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GearBox",
                table: "Cars",
                type: "TEXT",
                nullable: true);
        }
    }
}
