using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class ImageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "0d224f67-f188-40bd-8a1d-7c7d67ffc571" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "0d224f67-f188-40bd-8a1d-7c7d67ffc571");

            migrationBuilder.DropColumn(
                name: "IMAGE_DATA",
                table: "PRODUCTS");

            migrationBuilder.AddColumn<string>(
                name: "IMAGE_PATH",
                table: "PRODUCTS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "bc042b0b-70b5-4513-b531-a25d75c2b5db", "d591e4d6-4422-45b8-b697-deaa641bc801", new DateTime(2024, 6, 25, 7, 57, 20, 776, DateTimeKind.Utc).AddTicks(7002), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBuDz5jrkbTZnVZ83S2YztNZc9YBolT9yuuTFJkg7l4wom8CZkX1WuQ3ezb9Qr+Ulg==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 7, 57, 20, 776, DateTimeKind.Utc).AddTicks(7003), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "bc042b0b-70b5-4513-b531-a25d75c2b5db" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "bc042b0b-70b5-4513-b531-a25d75c2b5db" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "bc042b0b-70b5-4513-b531-a25d75c2b5db");

            migrationBuilder.DropColumn(
                name: "IMAGE_PATH",
                table: "PRODUCTS");

            migrationBuilder.AddColumn<byte[]>(
                name: "IMAGE_DATA",
                table: "PRODUCTS",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "0d224f67-f188-40bd-8a1d-7c7d67ffc571", "63e7d8e0-9cf5-4a19-baf8-248948237c3a", new DateTime(2024, 6, 25, 7, 6, 58, 23, DateTimeKind.Utc).AddTicks(8751), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEGCSC63TU0vIT7kQug5OtzCl/GxDrtLI/P68BO+9Q2lGHYBN+GBWCDnzBi05KENSew==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 7, 6, 58, 23, DateTimeKind.Utc).AddTicks(8752), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "0d224f67-f188-40bd-8a1d-7c7d67ffc571" });
        }
    }
}
