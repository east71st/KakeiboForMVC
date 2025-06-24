using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KakeiboForMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HIMOKU",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HIMOKU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "KAKEIBO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HIDUKE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HIMOKU_ID = table.Column<int>(type: "int", nullable: false),
                    MEISAI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NYUKINGAKU = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    SHUKINGAKU = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KAKEIBO", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HIMOKU");

            migrationBuilder.DropTable(
                name: "KAKEIBO");
        }
    }
}
