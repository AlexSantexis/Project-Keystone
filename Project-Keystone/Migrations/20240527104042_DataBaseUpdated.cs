using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Keystone.Migrations
{
    /// <inheritdoc />
    public partial class DataBaseUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLES_ROLES_ROLE_ID",
                table: "USER_ROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLES_USERS_USER_ID",
                table: "USER_ROLES");

            migrationBuilder.DropIndex(
                name: "UQ_USERNAME",
                table: "USERS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES");

            migrationBuilder.DropIndex(
                name: "IX_USER_ROLES_USER_ID_ROLE_ID",
                table: "USER_ROLES");

            migrationBuilder.DropColumn(
                name: "USER_ROLE_ID",
                table: "USER_ROLES");

            migrationBuilder.RenameColumn(
                name: "USER_NAME",
                table: "USERS",
                newName: "USERNAME");

            migrationBuilder.RenameColumn(
                name: "ROLE",
                table: "USERS",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "USER_ID",
                table: "USER_ROLES",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ROLE_ID",
                table: "USER_ROLES",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_USER_ROLES_ROLE_ID",
                table: "USER_ROLES",
                newName: "IX_USER_ROLES_RoleId");

            migrationBuilder.AlterColumn<string>(
                name: "PASSWORD_HASH",
                table: "USERS",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "USERNAME",
                table: "USERS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "USERS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "USERS",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "USERS",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ROLE_NAME",
                table: "ROLES",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ROLES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "ROLES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLES_ROLES_RoleId",
                table: "USER_ROLES",
                column: "RoleId",
                principalTable: "ROLES",
                principalColumn: "ROLE_ID",
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
                name: "FK_USER_ROLES_ROLES_RoleId",
                table: "USER_ROLES");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_ROLES_USERS_UserId",
                table: "USER_ROLES");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ROLES");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "ROLES");

            migrationBuilder.RenameColumn(
                name: "USERNAME",
                table: "USERS",
                newName: "USER_NAME");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "USERS",
                newName: "ROLE");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "USER_ROLES",
                newName: "ROLE_ID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "USER_ROLES",
                newName: "USER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_USER_ROLES_RoleId",
                table: "USER_ROLES",
                newName: "IX_USER_ROLES_ROLE_ID");

            migrationBuilder.AlterColumn<string>(
                name: "PASSWORD_HASH",
                table: "USERS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldMaxLength: 512,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "USER_NAME",
                table: "USERS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "USER_ROLE_ID",
                table: "USER_ROLES",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ROLE_NAME",
                table: "ROLES",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_ROLES",
                table: "USER_ROLES",
                column: "USER_ROLE_ID");

            migrationBuilder.CreateIndex(
                name: "UQ_USERNAME",
                table: "USERS",
                column: "USER_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_ROLES_USER_ID_ROLE_ID",
                table: "USER_ROLES",
                columns: new[] { "USER_ID", "ROLE_ID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLES_ROLES_ROLE_ID",
                table: "USER_ROLES",
                column: "ROLE_ID",
                principalTable: "ROLES",
                principalColumn: "ROLE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_ROLES_USERS_USER_ID",
                table: "USER_ROLES",
                column: "USER_ID",
                principalTable: "USERS",
                principalColumn: "USER_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
