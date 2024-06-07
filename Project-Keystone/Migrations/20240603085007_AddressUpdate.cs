using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class AddressUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SHIPPING_ADDRESS",
                table: "ORDERS",
                newName: "ZIP_CODE");

            migrationBuilder.AddColumn<string>(
                name: "CITY",
                table: "ORDERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "COUNTRY",
                table: "ORDERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "STREET_ADDRESS",
                table: "ORDERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ADDRESSES",
                columns: table => new
                {
                    ADDRESS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STREET_ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CITY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADDRESSES", x => x.ADDRESS_ID);
                    table.ForeignKey(
                        name: "FK_ADDRESSES_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "USER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ADDRESSES_USER_ID",
                table: "ADDRESSES",
                column: "USER_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADDRESSES");

            migrationBuilder.DropColumn(
                name: "CITY",
                table: "ORDERS");

            migrationBuilder.DropColumn(
                name: "COUNTRY",
                table: "ORDERS");

            migrationBuilder.DropColumn(
                name: "STREET_ADDRESS",
                table: "ORDERS");

            migrationBuilder.RenameColumn(
                name: "ZIP_CODE",
                table: "ORDERS",
                newName: "SHIPPING_ADDRESS");
        }
    }
}
