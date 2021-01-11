using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class updateIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2f362d09-3811-4fbe-a7e0-4263d968b0af");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "fd3413ef-dd10-4a8a-941c-bfed0f636085");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f362d09-3811-4fbe-a7e0-4263d968b0af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd3413ef-dd10-4a8a-941c-bfed0f636085");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ad9c51b-f95a-4a8b-8096-80f471b33776", "f0e70545-0463-4660-879b-f0d9d04eab63", "User", null },
                    { "a52c3cfd-7784-4e1a-bb1f-bafb5a497cb8", "949a7dbd-a5ec-4087-8456-80519b3582a6", "Admin", null }
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 1, 11, 10, 46, 37, 835, DateTimeKind.Local).AddTicks(5681));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created", "production" },
                values: new object[] { new DateTime(2021, 1, 11, 10, 46, 37, 838, DateTimeKind.Local).AddTicks(3838), new DateTime(2021, 1, 11, 10, 46, 37, 838, DateTimeKind.Local).AddTicks(4607) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created", "production" },
                values: new object[] { new DateTime(2021, 1, 11, 10, 46, 37, 838, DateTimeKind.Local).AddTicks(6427), new DateTime(2021, 1, 11, 10, 46, 37, 838, DateTimeKind.Local).AddTicks(6438) });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Id",
                value: "6ad9c51b-f95a-4a8b-8096-80f471b33776");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Id",
                value: "a52c3cfd-7784-4e1a-bb1f-bafb5a497cb8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "6ad9c51b-f95a-4a8b-8096-80f471b33776");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a52c3cfd-7784-4e1a-bb1f-bafb5a497cb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ad9c51b-f95a-4a8b-8096-80f471b33776");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a52c3cfd-7784-4e1a-bb1f-bafb5a497cb8");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "fd3413ef-dd10-4a8a-941c-bfed0f636085", "fcb0513c-f2da-4dae-a74f-b81d4f3fd6b3", "User", null },
                    { "2f362d09-3811-4fbe-a7e0-4263d968b0af", "00bd809e-be22-4b77-9f55-02e4c3956c00", "Admin", null }
                });

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2021, 1, 10, 18, 17, 35, 789, DateTimeKind.Local).AddTicks(6445));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "Created", "production" },
                values: new object[] { new DateTime(2021, 1, 10, 18, 17, 35, 792, DateTimeKind.Local).AddTicks(5577), new DateTime(2021, 1, 10, 18, 17, 35, 792, DateTimeKind.Local).AddTicks(6312) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "Created", "production" },
                values: new object[] { new DateTime(2021, 1, 10, 18, 17, 35, 792, DateTimeKind.Local).AddTicks(8374), new DateTime(2021, 1, 10, 18, 17, 35, 792, DateTimeKind.Local).AddTicks(8385) });

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Id",
                value: "fd3413ef-dd10-4a8a-941c-bfed0f636085");

            migrationBuilder.InsertData(
                table: "Roles",
                column: "Id",
                value: "2f362d09-3811-4fbe-a7e0-4263d968b0af");
        }
    }
}
