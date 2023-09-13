using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yado_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRequestRoleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleRequests_Roles_RequestedRoleId",
                table: "UserRoleRequests");

            migrationBuilder.RenameColumn(
                name: "RequestedRoleId",
                table: "UserRoleRequests",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleRequests_RequestedRoleId",
                table: "UserRoleRequests",
                newName: "IX_UserRoleRequests_RoleId");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelId",
                table: "Favorites",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(255)",
                oldMaxLength: 255)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Favorites",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(40)",
                oldMaxLength: 40)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleRequests_Roles_RoleId",
                table: "UserRoleRequests",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleRequests_Roles_RoleId",
                table: "UserRoleRequests");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoleRequests",
                newName: "RequestedRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoleRequests_RoleId",
                table: "UserRoleRequests",
                newName: "IX_UserRoleRequests_RequestedRoleId");

            migrationBuilder.AlterColumn<string>(
                name: "HotelId",
                table: "Favorites",
                type: "char(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "char(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(40)",
                oldMaxLength: 40)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleRequests_Roles_RequestedRoleId",
                table: "UserRoleRequests",
                column: "RequestedRoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
