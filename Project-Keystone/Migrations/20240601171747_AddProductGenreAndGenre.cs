using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class AddProductGenreAndGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ_EMAIL",
                table: "USERS");

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "USERS",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "GENRES",
                columns: table => new
                {
                    GENRE_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENRES", x => x.GENRE_ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_GENRES",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_GENRES", x => new { x.ProductId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_PRODUCT_GENRES_GENRES_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GENRES",
                        principalColumn: "GENRE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRODUCT_GENRES_PRODUCTS_ProductId",
                        column: x => x.ProductId,
                        principalTable: "PRODUCTS",
                        principalColumn: "PRODUCT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true,
                filter: "[EMAIL] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GENRES_NAME",
                table: "GENRES",
                column: "NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_GENRES_GenreId",
                table: "PRODUCT_GENRES",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCT_GENRES");

            migrationBuilder.DropTable(
                name: "GENRES");

            migrationBuilder.DropIndex(
                name: "UQ_EMAIL",
                table: "USERS");

            migrationBuilder.AlterColumn<string>(
                name: "EMAIL",
                table: "USERS",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EMAIL",
                table: "USERS",
                column: "EMAIL",
                unique: true);
        }
    }
}
