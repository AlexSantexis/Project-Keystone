using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CONCURRENCY_STAMP",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NORMALIZED_USER_NAME",
                table: "USERS",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "USERS",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "USERS",
                column: "NORMALIZED_USER_NAME",
                unique: true,
                filter: "[NORMALIZED_USER_NAME] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "CONCURRENCY_STAMP",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NORMALIZED_USER_NAME",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "USERS");
        }
    }
}
