using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HandwritingService.DAL.Migrations
{
    public partial class SimplifiedDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 5, 5, 11, 53, 15, 927, DateTimeKind.Local).AddTicks(9961), new DateTime(2020, 5, 5, 11, 53, 15, 933, DateTimeKind.Local).AddTicks(8342) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 28, 16, 37, 21, 857, DateTimeKind.Local).AddTicks(1306), new DateTime(2020, 4, 28, 16, 37, 21, 863, DateTimeKind.Local).AddTicks(407) });
        }
    }
}
