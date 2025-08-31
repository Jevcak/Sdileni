using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Sessions",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Sessions",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sessions",
                newName: "DateTime");
        }
    }
}
