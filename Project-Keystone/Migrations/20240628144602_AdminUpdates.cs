using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class AdminUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "9c087904-f909-4ce0-8f73-1d3fe8404ca3" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "9c087904-f909-4ce0-8f73-1d3fe8404ca3");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "720f3eff-b608-4633-801e-ada6b4e7b2f4", "338fb339-70f5-4d24-ba35-50305ff4b211", new DateTime(2024, 6, 28, 14, 46, 1, 821, DateTimeKind.Utc).AddTicks(3064), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAWiwQ7qOxQnlBOo8ZxMxy8nQW/zg3kLmXfoAg8EygvPweA4bZc44YOkFlAtbcpzSw==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 28, 14, 46, 1, 821, DateTimeKind.Utc).AddTicks(3066), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "720f3eff-b608-4633-801e-ada6b4e7b2f4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "720f3eff-b608-4633-801e-ada6b4e7b2f4" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "720f3eff-b608-4633-801e-ada6b4e7b2f4");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "9c087904-f909-4ce0-8f73-1d3fe8404ca3", "a076450b-2a38-444b-ab09-859354a740bd", new DateTime(2024, 6, 28, 12, 38, 30, 798, DateTimeKind.Utc).AddTicks(9191), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEPnkmgzJlImw5pOzlGQP21UhvtXeid3HSdz+dCGPy6boVVTyQ3Yf+FVkwIu+pzpBJQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 28, 12, 38, 30, 798, DateTimeKind.Utc).AddTicks(9192), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "9c087904-f909-4ce0-8f73-1d3fe8404ca3" });
        }
    }
}
