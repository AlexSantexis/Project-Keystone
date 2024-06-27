using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "17fd6731-1446-4a61-b0b2-b91dec0b35b6" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "17fd6731-1446-4a61-b0b2-b91dec0b35b6");

            migrationBuilder.RenameColumn(
                name: "IMAGE_PATH",
                table: "PRODUCTS",
                newName: "IMAGE_URL");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "899cc55b-4b08-4335-a30b-6e82e6d8aace", "2acb709f-c11c-4fd9-acc6-1c8e1393c475", new DateTime(2024, 6, 26, 4, 52, 57, 487, DateTimeKind.Utc).AddTicks(4104), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEAI+89fr69lk26Q6zUsgwNTyywBAWEGYK8PSEBIkfHyhMB2P7yBaxQikX/hLJB9zEQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 26, 4, 52, 57, 487, DateTimeKind.Utc).AddTicks(4105), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "899cc55b-4b08-4335-a30b-6e82e6d8aace" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "899cc55b-4b08-4335-a30b-6e82e6d8aace" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "899cc55b-4b08-4335-a30b-6e82e6d8aace");

            migrationBuilder.RenameColumn(
                name: "IMAGE_URL",
                table: "PRODUCTS",
                newName: "IMAGE_PATH");

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "17fd6731-1446-4a61-b0b2-b91dec0b35b6", "314339c8-dbe0-4985-a00e-945f54b9d5b0", new DateTime(2024, 6, 25, 9, 0, 40, 360, DateTimeKind.Utc).AddTicks(1586), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA2Ghrx1JcFHELcWHDIm1qdERGqE4BZL4JanMZVEVSjVZwB4I5ZgSC6Z7gBFaX0wCQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 9, 0, 40, 360, DateTimeKind.Utc).AddTicks(1588), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "17fd6731-1446-4a61-b0b2-b91dec0b35b6" });
        }
    }
}
