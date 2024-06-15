using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class RenamedIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_USERS_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_CLAIMS_AspNetRoles_RoleId",
                table: "ROLE_CLAIMS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "USER_ROLES");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "ROLES");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "USER_ROLES",
                newName: "IX_USER_ROLES_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLES",
                table: "ROLES",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_CLAIMS_ROLES_RoleId",
                table: "ROLE_CLAIMS",
                column: "RoleId",
                principalTable: "ROLES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLES_ROLES_RoleId",
                table: "USER_ROLES",
                column: "RoleId",
                principalTable: "ROLES",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLES_USERS_UserId",
                table: "USER_ROLES",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLE_CLAIMS_ROLES_RoleId",
                table: "ROLE_CLAIMS");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLES_ROLES_RoleId",
                table: "USER_ROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLES_USERS_UserId",
                table: "USER_ROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLES",
                table: "ROLES");

            migrationBuilder.RenameTable(
                name: "USER_ROLES",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "ROLES",
                newName: "AspNetRoles");

            migrationBuilder.RenameIndex(
                name: "IX_USER_ROLES_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_USERS_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ROLE_CLAIMS_AspNetRoles_RoleId",
                table: "ROLE_CLAIMS",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
