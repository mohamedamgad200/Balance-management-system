using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class intial33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_depoists",
                table: "depoists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_suoeradmins",
                table: "suoeradmins");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "banks");

            migrationBuilder.RenameTable(
                name: "depoists",
                newName: "Depoists");

            migrationBuilder.RenameTable(
                name: "suoeradmins",
                newName: "superAdmins");

            migrationBuilder.RenameColumn(
                name: "PictureUrl",
                table: "Depoists",
                newName: "FileName");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Bankid",
                table: "Depoists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Companyid",
                table: "Depoists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Depoists",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "banks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "superAdmins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Depoists",
                table: "Depoists",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_superAdmins",
                table: "superAdmins",
                column: "id");

            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    balance = table.Column<float>(type: "real", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depoists_Bankid",
                table: "Depoists",
                column: "Bankid");

            migrationBuilder.CreateIndex(
                name: "IX_Depoists_Companyid",
                table: "Depoists",
                column: "Companyid");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_banks_Bankid",
                table: "Depoists");

            migrationBuilder.DropForeignKey(
                name: "FK_Depoists_companies_Companyid",
                table: "Depoists");

            migrationBuilder.DropTable(
                name: "companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Depoists",
                table: "Depoists");

            migrationBuilder.DropIndex(
                name: "IX_Depoists_Bankid",
                table: "Depoists");

            migrationBuilder.DropIndex(
                name: "IX_Depoists_Companyid",
                table: "Depoists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_superAdmins",
                table: "superAdmins");

            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Bankid",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Companyid",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Depoists");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "banks");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "superAdmins");

            migrationBuilder.RenameTable(
                name: "Depoists",
                newName: "depoists");

            migrationBuilder.RenameTable(
                name: "superAdmins",
                newName: "suoeradmins");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "depoists",
                newName: "PictureUrl");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "depoists",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AccountNumber",
                table: "banks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_depoists",
                table: "depoists",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_suoeradmins",
                table: "suoeradmins",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    balance = table.Column<float>(type: "real", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });
        }
    }
}
