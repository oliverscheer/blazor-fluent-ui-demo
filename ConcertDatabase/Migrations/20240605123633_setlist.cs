using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertDatabase.Migrations
{
    /// <inheritdoc />
    public partial class setlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SetList",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Concerts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ID", "ArtistID", "City", "Date", "Description", "Name", "SetList", "Url", "Venue" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000003"), "Munich", new DateTime(2024, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metallica's M72 Tour", "M72 Tour", null, null, "Olympiastadion" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000003"), "Munich", new DateTime(2024, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Metallica's M72 Tour", "M72 Tour", null, null, "Olympiastadion" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ID",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "ID",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DropColumn(
                name: "SetList",
                table: "Concerts");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Concerts");
        }
    }
}
