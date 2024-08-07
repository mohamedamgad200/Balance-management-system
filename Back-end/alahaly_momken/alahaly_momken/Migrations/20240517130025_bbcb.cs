using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace alahaly_momken.Migrations
{
    public partial class bbcb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "corrections",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    Bankid = table.Column<int>(type: "int", nullable: false),
                    blanaceafter = table.Column<float>(type: "real", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    balancebefore = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_corrections", x => x.id);
                    table.ForeignKey(
                        name: "FK_corrections_banks_Bankid",
                        column: x => x.Bankid,
                        principalTable: "banks",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_corrections_Bankid",
                table: "corrections",
                column: "Bankid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "corrections");
        }
    }
}
