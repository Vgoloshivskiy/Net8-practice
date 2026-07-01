using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87ebe6c3-e0e1-42b2-b6df-f00f898c52ab", "a3fb7e68-3c77-47bd-83ab-c85d298765ea", "User", "USER" },
                    { "b5500ef2-d8d3-48ce-ba85-05aee1a4255f", "1cf0920b-f127-49c2-b0cc-b567c4b26d3d", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87ebe6c3-e0e1-42b2-b6df-f00f898c52ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5500ef2-d8d3-48ce-ba85-05aee1a4255f");
        }
    }
}
