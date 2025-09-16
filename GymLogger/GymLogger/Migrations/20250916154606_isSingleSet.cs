using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Migrations
{
    /// <inheritdoc />
    public partial class isSingleSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSingleSet",
                table: "ExerciseSessions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSingleSet",
                table: "ExerciseSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
