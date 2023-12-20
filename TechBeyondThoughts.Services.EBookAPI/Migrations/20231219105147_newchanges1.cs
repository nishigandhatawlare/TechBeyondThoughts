using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechBeyondThoughts.Services.EBookAPI.Migrations
{
    /// <inheritdoc />
    public partial class newchanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookstacks",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Format = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Downloads = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookstacks", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "BookPreviews",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PagesToPreview = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPreviews", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookPreviews_Bookstacks_BookId",
                        column: x => x.BookId,
                        principalTable: "Bookstacks",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DownloadHistory",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DownloadDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadHistory", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_DownloadHistory_Bookstacks_BookId",
                        column: x => x.BookId,
                        principalTable: "Bookstacks",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Ratings_Bookstacks_BookId",
                        column: x => x.BookId,
                        principalTable: "Bookstacks",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bookstacks",
                columns: new[] { "BookId", "Author", "Category", "Description", "Downloads", "FileName", "FilePath", "Format", "Pages", "Published", "Rating", "Title" },
                values: new object[] { 1, "Author 1", "Fiction", "Description 1", 100, "book1.pdf", "/storage/book1.pdf", "PDF", 200, new DateTime(2023, 12, 19, 16, 21, 46, 836, DateTimeKind.Local).AddTicks(4886), 4.5, "Book 1" });

            migrationBuilder.InsertData(
                table: "BookPreviews",
                columns: new[] { "BookId", "PagesToPreview" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "DownloadHistory",
                columns: new[] { "BookId", "DownloadDateTime", "UserId" },
                values: new object[] { 1, new DateTime(2023, 12, 19, 16, 21, 46, 836, DateTimeKind.Local).AddTicks(5158), 1 });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "BookId", "UserId", "Value" },
                values: new object[] { 1, 1, 4.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPreviews");

            migrationBuilder.DropTable(
                name: "DownloadHistory");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Bookstacks");
        }
    }
}
