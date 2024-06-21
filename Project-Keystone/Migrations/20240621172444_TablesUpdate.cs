using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class TablesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CATEGORIES_NAME",
                table: "CATEGORIES");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_NAME",
                table: "CATEGORIES",
                column: "NAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CATEGORIES_NAME",
                table: "CATEGORIES");

            migrationBuilder.CreateIndex(
                name: "IX_CATEGORIES_NAME",
                table: "CATEGORIES",
                column: "NAME");
        }
    }
}
