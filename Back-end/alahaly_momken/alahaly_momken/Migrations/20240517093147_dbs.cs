using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class dbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bankid",
                table: "debts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Companyid",
                table: "debts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "debts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "balancebefore",
                table: "debts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "blanaceafter",
                table: "debts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "balance",
                table: "banks",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_debts_Bankid",
                table: "debts",
                column: "Bankid");

            migrationBuilder.CreateIndex(
                name: "IX_debts_Companyid",
                table: "debts",
                column: "Companyid");

            migrationBuilder.AddForeignKey(
                name: "FK_debts_banks_Bankid",
                table: "debts",
                column: "Bankid",
                principalTable: "banks",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_debts_companies_Companyid",
                table: "debts",
                column: "Companyid",
                principalTable: "companies",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_debts_banks_Bankid",
                table: "debts");

            migrationBuilder.DropForeignKey(
                name: "FK_debts_companies_Companyid",
                table: "debts");

            migrationBuilder.DropIndex(
                name: "IX_debts_Bankid",
                table: "debts");

            migrationBuilder.DropIndex(
                name: "IX_debts_Companyid",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "Bankid",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "Companyid",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "balancebefore",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "blanaceafter",
                table: "debts");

            migrationBuilder.DropColumn(
                name: "balance",
                table: "banks");
        }
    }
}
