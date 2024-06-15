using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRolesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "ROLES",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ROLES",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "ROLES");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "ROLES");
        }
    }
}
