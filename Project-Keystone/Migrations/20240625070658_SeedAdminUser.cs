using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "0d224f67-f188-40bd-8a1d-7c7d67ffc571", "63e7d8e0-9cf5-4a19-baf8-248948237c3a", new DateTime(2024, 6, 25, 7, 6, 58, 23, DateTimeKind.Utc).AddTicks(8751), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGCSC63TU0vIT7kQug5OtzCl/GxDrtLI/P68BO+9Q2lGHYBN+GBWCDnzBi05KENSew==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 7, 6, 58, 23, DateTimeKind.Utc).AddTicks(8752), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "0d224f67-f188-40bd-8a1d-7c7d67ffc571" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "0d224f67-f188-40bd-8a1d-7c7d67ffc571" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "0d224f67-f188-40bd-8a1d-7c7d67ffc571");
        }
    }
}
