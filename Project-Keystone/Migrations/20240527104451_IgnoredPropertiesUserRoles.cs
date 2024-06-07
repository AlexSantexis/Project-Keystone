using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class IgnoredPropertiesUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ROLES");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "ROLES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "USERS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "USERS",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ROLES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "ROLES",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
