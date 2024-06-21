using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMAGE_URL",
                table: "PRODUCTS");

            migrationBuilder.AddColumn<byte[]>(
                name: "IMAGE_DATA",
                table: "PRODUCTS",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "CATEGORIES",
                columns: new[] { "CATEGORY_ID", "NAME" },
                values: new object[,]
                {
                    { 1, "PC" },
                    { 2, "PSN" },
                    { 3, "Xbox" },
                    { 4, "Nintendo" }
                });

            migrationBuilder.InsertData(
                table: "GENRES",
                columns: new[] { "GENRE_ID", "NAME" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Singleplayer" },
                    { 4, "Strategy" },
                    { 5, "Multiplayer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CATEGORIES",
                keyColumn: "CATEGORY_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CATEGORIES",
                keyColumn: "CATEGORY_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CATEGORIES",
                keyColumn: "CATEGORY_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CATEGORIES",
                keyColumn: "CATEGORY_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GENRES",
                keyColumn: "GENRE_ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GENRES",
                keyColumn: "GENRE_ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GENRES",
                keyColumn: "GENRE_ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GENRES",
                keyColumn: "GENRE_ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GENRES",
                keyColumn: "GENRE_ID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "IMAGE_DATA",
                table: "PRODUCTS");

            migrationBuilder.AddColumn<string>(
                name: "IMAGE_URL",
                table: "PRODUCTS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
