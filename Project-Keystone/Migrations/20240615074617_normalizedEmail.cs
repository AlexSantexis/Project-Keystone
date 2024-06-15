using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class normalizedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "USERS",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "USERS",
                column: "NormalizedEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "USERS");
        }
    }
}
