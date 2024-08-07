using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class bb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bankname",
                table: "Depoists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bankname",
                table: "Depoists");
        }
    }
}
