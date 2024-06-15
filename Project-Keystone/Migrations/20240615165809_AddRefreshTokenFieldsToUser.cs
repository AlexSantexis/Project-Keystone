using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "USERS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "USERS");
        }
    }
}
