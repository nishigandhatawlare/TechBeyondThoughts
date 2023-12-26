using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBeyondThoughts.Services.EBookAPI.Migrations
{
    /// <inheritdoc />
    public partial class newEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookPreviews",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "BookPreviews",
                columns: new[] { "BookId", "PagesToPreview" },
                values: new object[] { 4, 5 });

            migrationBuilder.UpdateData(
                table: "Bookstacks",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Published",
                value: new DateTime(2023, 12, 23, 12, 38, 12, 790, DateTimeKind.Local).AddTicks(1169));

            migrationBuilder.UpdateData(
                table: "DownloadHistory",
                keyColumn: "BookId",
                keyValue: 1,
                column: "DownloadDateTime",
                value: new DateTime(2023, 12, 23, 12, 38, 12, 790, DateTimeKind.Local).AddTicks(1576));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BookPreviews",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "BookPreviews",
                columns: new[] { "BookId", "PagesToPreview" },
                values: new object[] { 1, 5 });

            migrationBuilder.UpdateData(
                table: "Bookstacks",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Published",
                value: new DateTime(2023, 12, 20, 12, 50, 15, 428, DateTimeKind.Local).AddTicks(3175));

            migrationBuilder.UpdateData(
                table: "DownloadHistory",
                keyColumn: "BookId",
                keyValue: 1,
                column: "DownloadDateTime",
                value: new DateTime(2023, 12, 20, 12, 50, 15, 428, DateTimeKind.Local).AddTicks(3467));
        }
    }
}
