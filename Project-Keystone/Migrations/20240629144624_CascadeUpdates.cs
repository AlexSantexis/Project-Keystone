using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class CascadeUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "f0ded707-eebc-481a-ae49-260a9855d0e4" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "f0ded707-eebc-481a-ae49-260a9855d0e4");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "491ee86a-a604-4a6b-9625-64baaf396bf3", "2ace200f-3ee2-49ed-a9d1-cd6d15003671", new DateTime(2024, 6, 29, 14, 46, 24, 640, DateTimeKind.Utc).AddTicks(1927), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEMhrvDC6MTVlFOTdd+BUwGaQrE5HBKUTWzQp1ZESr5n9J5NQooBofu/mGRHBjuP2IQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 29, 14, 46, 24, 640, DateTimeKind.Utc).AddTicks(1929), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "491ee86a-a604-4a6b-9625-64baaf396bf3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "491ee86a-a604-4a6b-9625-64baaf396bf3" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "491ee86a-a604-4a6b-9625-64baaf396bf3");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "f0ded707-eebc-481a-ae49-260a9855d0e4", "6ddb2035-59a9-466a-a3c5-bfd337705647", new DateTime(2024, 6, 29, 14, 43, 25, 993, DateTimeKind.Utc).AddTicks(9467), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA8pObcEzh5vl21tGAyfVttwHfC9BIg0zNHq7RMmo4qf0Hw7urCagPaHxz2dEg161A==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 29, 14, 43, 25, 993, DateTimeKind.Utc).AddTicks(9470), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "f0ded707-eebc-481a-ae49-260a9855d0e4" });
        }
    }
}
