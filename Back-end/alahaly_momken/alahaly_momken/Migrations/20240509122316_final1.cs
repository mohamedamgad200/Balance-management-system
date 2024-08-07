using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class final1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_debits",
                table: "debits");

            migrationBuilder.RenameTable(
                name: "debits",
                newName: "debts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_debts",
                table: "debts",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_debts",
                table: "debts");

            migrationBuilder.RenameTable(
                name: "debts",
                newName: "debits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_debits",
                table: "debits",
                column: "Id");
        }
    }
}
