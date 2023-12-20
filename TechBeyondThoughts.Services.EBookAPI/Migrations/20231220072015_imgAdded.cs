using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBeyondThoughts.Services.EBookAPI.Migrations
{
    /// <inheritdoc />
    public partial class imgAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Bookstacks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Bookstacks",
                keyColumn: "BookId",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Published" },
                values: new object[] { "https://images.unsplash.com/photo-1543002588-bfa74002ed7e?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxleHBsb3JlLWZlZWR8Mnx8fGVufDB8fHx8fA%3D%3D", new DateTime(2023, 12, 20, 12, 50, 15, 428, DateTimeKind.Local).AddTicks(3175) });

            migrationBuilder.UpdateData(
                table: "DownloadHistory",
                keyColumn: "BookId",
                keyValue: 1,
                column: "DownloadDateTime",
                value: new DateTime(2023, 12, 20, 12, 50, 15, 428, DateTimeKind.Local).AddTicks(3467));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Bookstacks");

            migrationBuilder.UpdateData(
                table: "Bookstacks",
                keyColumn: "BookId",
                keyValue: 1,
                column: "Published",
                value: new DateTime(2023, 12, 19, 16, 21, 46, 836, DateTimeKind.Local).AddTicks(4886));

            migrationBuilder.UpdateData(
                table: "DownloadHistory",
                keyColumn: "BookId",
                keyValue: 1,
                column: "DownloadDateTime",
                value: new DateTime(2023, 12, 19, 16, 21, 46, 836, DateTimeKind.Local).AddTicks(5158));
        }
    }
}
