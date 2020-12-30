using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    production = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "id", "Created", "name" },
                values: new object[] { 1, new DateTime(2020, 12, 29, 11, 34, 7, 848, DateTimeKind.Local).AddTicks(8229), "Company1" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "CompanyId", "Created", "name", "production" },
                values: new object[] { 1, 1, new DateTime(2020, 12, 29, 11, 34, 7, 851, DateTimeKind.Local).AddTicks(2863), "product 1", new DateTime(2020, 12, 29, 11, 34, 7, 851, DateTimeKind.Local).AddTicks(3491) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "id", "CompanyId", "Created", "name", "production" },
                values: new object[] { 2, 1, new DateTime(2020, 12, 29, 11, 34, 7, 851, DateTimeKind.Local).AddTicks(4608), "product 2", new DateTime(2020, 12, 29, 11, 34, 7, 851, DateTimeKind.Local).AddTicks(4617) });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
