using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class ProductCategoryUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.DropIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "bc042b0b-70b5-4513-b531-a25d75c2b5db" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "bc042b0b-70b5-4513-b531-a25d75c2b5db");

            migrationBuilder.DropColumn(
                name: "CATEGORY_ID",
                table: "PRODUCTS");

            migrationBuilder.CreateTable(
                name: "PRODUCT_CATEGORIES",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_CATEGORIES", x => new { x.ProductId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORIES_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CATEGORIES",
                        principalColumn: "CATEGORY_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_CATEGORIES_PRODUCTS_ProductId",
                        column: x => x.ProductId,
                        principalTable: "PRODUCTS",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "17fd6731-1446-4a61-b0b2-b91dec0b35b6", "314339c8-dbe0-4985-a00e-945f54b9d5b0", new DateTime(2024, 6, 25, 9, 0, 40, 360, DateTimeKind.Utc).AddTicks(1586), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEA2Ghrx1JcFHELcWHDIm1qdERGqE4BZL4JanMZVEVSjVZwB4I5ZgSC6Z7gBFaX0wCQ==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 9, 0, 40, 360, DateTimeKind.Utc).AddTicks(1588), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "17fd6731-1446-4a61-b0b2-b91dec0b35b6" });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_CATEGORIES_CategoryId",
                table: "PRODUCT_CATEGORIES",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCT_CATEGORIES");

            migrationBuilder.DeleteData(
                table: "USER_ROLES",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "17fd6731-1446-4a61-b0b2-b91dec0b35b6" });

            migrationBuilder.DeleteData(
                table: "USERS",
                keyColumn: "Id",
                keyValue: "17fd6731-1446-4a61-b0b2-b91dec0b35b6");

            migrationBuilder.AddColumn<int>(
                name: "CATEGORY_ID",
                table: "PRODUCTS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "USERS",
                columns: new[] { "Id", "CONCURRENCY_STAMP", "CREATED_AT", "EMAIL", "FIRSTNAME", "LASTNAME", "NormalizedEmail", "NORMALIZED_USER_NAME", "PASSWORD_HASH", "RefreshToken", "RefreshTokenExpiryTime", "UPDATED_AT", "UserName" },
                values: new object[] { "bc042b0b-70b5-4513-b531-a25d75c2b5db", "d591e4d6-4422-45b8-b697-deaa641bc801", new DateTime(2024, 6, 25, 7, 57, 20, 776, DateTimeKind.Utc).AddTicks(7002), "admin@example.com", "Admin", "User", "ADMIN@EXAMPLE.COM", "ADMIN@EXAMPLE.COM", "AQAAAAIAAYagAAAAEBuDz5jrkbTZnVZ83S2YztNZc9YBolT9yuuTFJkg7l4wom8CZkX1WuQ3ezb9Qr+Ulg==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 6, 25, 7, 57, 20, 776, DateTimeKind.Utc).AddTicks(7003), "admin@example.com" });

            migrationBuilder.InsertData(
                table: "USER_ROLES",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dda0e414-944b-4c35-804b-4e4784abc301", "bc042b0b-70b5-4513-b531-a25d75c2b5db" });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PRODUCTS_CATEGORIES_CATEGORY_ID",
                table: "PRODUCTS",
                column: "CATEGORY_ID",
                principalTable: "CATEGORIES",
                principalColumn: "CATEGORY_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
