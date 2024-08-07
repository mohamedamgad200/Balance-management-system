using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class intial4543 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Depoists");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Depoists",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Depoists",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Depoists",
                newName: "ImagePath");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Depoists",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Depoists",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Depoists",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Depoists",
                newName: "FileName");

            migrationBuilder.AlterColumn<float>(
                name: "Amount",
                table: "Depoists",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Depoists",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
