using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HandwritingService.DAL.Migrations
{
    public partial class AddedContentToHandwriting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Handwritings",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 28, 16, 24, 33, 479, DateTimeKind.Local).AddTicks(5896), new DateTime(2020, 4, 28, 16, 24, 33, 482, DateTimeKind.Local).AddTicks(9678) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Handwritings");

            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 9, 14, 34, 32, 542, DateTimeKind.Local).AddTicks(5379), new DateTime(2020, 4, 9, 14, 34, 32, 546, DateTimeKind.Local).AddTicks(2531) });
        }
    }
}
