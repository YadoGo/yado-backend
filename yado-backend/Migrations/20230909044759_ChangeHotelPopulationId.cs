using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace yado_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHotelPopulationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParameterId",
                table: "Hotels");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "varchar(5000)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(1500)",
                oldMaxLength: 1500)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Hotels",
                type: "varchar(1500)",
                maxLength: 1500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(5000)",
                oldMaxLength: 5000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ParameterId",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
