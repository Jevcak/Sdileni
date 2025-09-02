using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymLogger.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseMuscles");

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthDate",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExerciseMuscles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 5 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 7 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 9 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 13 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 6 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 10 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 11 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 12 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 4, 6 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 4, 10 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 7 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 11 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 12 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 6 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 8, 8 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 8, 10 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 10, 4 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 11, 9 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 12, 2 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 13, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 13, 11 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 14, 7 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 14, 12 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 6 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 10 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 12 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 16, 7 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 16, 12 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 11 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 1 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 3 },
                column: "Id",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 11 },
                column: "Id",
                value: 0);
        }
    }
}
