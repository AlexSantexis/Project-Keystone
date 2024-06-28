using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class UserAddressupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADDRESSES_USERS_USER_ID",
                table: "ADDRESSES");

            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "899cc55b-4b08-4335-a30b-6e82e6d8aace" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "899cc55b-4b08-4335-a30b-6e82e6d8aace");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "04fee489-2bb3-456b-9777-63fab94b1836", "aee70ebc-7f31-4ae5-9878-12d29fa81004", new DateTime(2024, 6, 28, 6, 57, 38, 816, DateTimeKind.Utc).AddTicks(9618), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEF0U31XgYdRswPuwUAFI1cOGDXVpEl01ypuOVRDgXG4UvF/E7/kFaCC66LOBUqzr8w==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 28, 6, 57, 38, 816, DateTimeKind.Utc).AddTicks(9624), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "04fee489-2bb3-456b-9777-63fab94b1836" });

            migrationBuilder.AddForeignKey(
                name: "FK_ADDRESSES_USERS_USER_ID",
                table: "ADDRESSES",
                column: "USER_ID",
                principalTable: "USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ADDRESSES_USERS_USER_ID",
                table: "ADDRESSES");

            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "04fee489-2bb3-456b-9777-63fab94b1836" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "04fee489-2bb3-456b-9777-63fab94b1836");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "899cc55b-4b08-4335-a30b-6e82e6d8aace", "2acb709f-c11c-4fd9-acc6-1c8e1393c475", new DateTime(2024, 6, 26, 4, 52, 57, 487, DateTimeKind.Utc).AddTicks(4104), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAI+89fr69lk26Q6zUsgwNTyywBAWEGYK8PSEBIkfHyhMB2P7yBaxQikX/hLJB9zEQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 26, 4, 52, 57, 487, DateTimeKind.Utc).AddTicks(4105), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "899cc55b-4b08-4335-a30b-6e82e6d8aace" });

            migrationBuilder.AddForeignKey(
                name: "FK_ADDRESSES_USERS_USER_ID",
                table: "ADDRESSES",
                column: "USER_ID",
                principalTable: "USERS",
                principalColumn: "Id");
        }
    }
}
