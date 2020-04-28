using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HandwritingService.DAL.Migrations
{
    public partial class SimplifiedNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Handwritings_HandwritingId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_HandwritingId",
                table: "Notes");

            migrationBuilder.AddColumn<int>(
                name: "HandwritingId1",
                table: "Notes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Handwritings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 28, 16, 37, 21, 857, DateTimeKind.Local).AddTicks(1306), new DateTime(2020, 4, 28, 16, 37, 21, 863, DateTimeKind.Local).AddTicks(407) });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HandwritingId1",
                table: "Notes",
                column: "HandwritingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Handwritings_HandwritingId1",
                table: "Notes",
                column: "HandwritingId1",
                principalTable: "Handwritings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Handwritings_HandwritingId1",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_HandwritingId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "HandwritingId1",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Handwritings");

            migrationBuilder.UpdateData(
                table: "Handwritings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2020, 4, 28, 16, 24, 33, 479, DateTimeKind.Local).AddTicks(5896), new DateTime(2020, 4, 28, 16, 24, 33, 482, DateTimeKind.Local).AddTicks(9678) });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_HandwritingId",
                table: "Notes",
                column: "HandwritingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Handwritings_HandwritingId",
                table: "Notes",
                column: "HandwritingId",
                principalTable: "Handwritings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
