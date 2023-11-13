using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechBeyondThoughts.Services.TechAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Techstacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Techstacks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Techstacks",
                columns: new[] { "Id", "Category", "Description", "ImageUrl", "InventionDate", "InventorName", "Title" },
                values: new object[,]
                {
                    { 1, "Machine Learning", "A field of computer science that simulates human intelligence in machines.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Ffree-photos-vectors%2Fhr-ai%2F62&psig=AOvVaw3vymYJOV0QJXmKzYhn1jCM&ust=1699509512867000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCKjS67jcs4IDFQAAAAAdAAAAABAJ", new DateTime(1956, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "John McCarthy", "Artificial Intelligence" },
                    { 2, "Cryptocurrency", "A distributed ledger technology for secure and transparent transactions.", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.infoq.com%2Farticles%2Fblockchain-as-a-service-get-block%2F&psig=AOvVaw3jbqS1fPY4Bxv_HzE9cUwE&ust=1699509637212000&source=images&cd=vfe&opi=89978449&ved=0CBIQjRxqFwoTCMDmgfTcs4IDFQAAAAAdAAAAABAE", new DateTime(2008, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Satoshi Nakamoto", "Blockchain" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Techstacks");
        }
    }
}
