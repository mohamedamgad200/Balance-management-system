using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class intial707 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_banks_Bankid",
                table: "Depoists");

            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_companies_Companyid",
                table: "Depoists");

            migrationBuilder.AlterColumn<int>(
                name: "Companyid",
                table: "Depoists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Bankid",
                table: "Depoists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Transicationtype",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "debits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transicationtype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debits", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Depoists_banks_Bankid",
                table: "Depoists",
                column: "Bankid",
                principalTable: "banks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Depoists_companies_Companyid",
                table: "Depoists",
                column: "Companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_banks_Bankid",
                table: "Depoists");

            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_companies_Companyid",
                table: "Depoists");

            migrationBuilder.DropTable(
                name: "debits");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Transicationtype",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Depoists");

            migrationBuilder.AlterColumn<int>(
                name: "Companyid",
                table: "Depoists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Bankid",
                table: "Depoists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Depoists_banks_Bankid",
                table: "Depoists",
                column: "Bankid",
                principalTable: "banks",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Depoists_companies_Companyid",
                table: "Depoists",
                column: "Companyid",
                principalTable: "companies",
                principalColumn: "id");
        }
    }
}
