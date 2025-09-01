using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymLogger.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseMuscles",
                table: "ExerciseMuscles");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseMuscles_ExerciseId",
                table: "ExerciseMuscles");

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "ExerciseSessions",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NofSets",
                table: "ExerciseSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NofRepetitions",
                table: "ExerciseSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExerciseMuscles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseMuscles",
                table: "ExerciseMuscles",
                columns: new[] { "ExerciseId", "MuscleId" });

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bench Press");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Squat");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Deadlift");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Weighted Pull-ups");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Clean");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Bulgarian Split Squat");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Romanian Deadlift");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Biceps Curl");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Weighted Lunges");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Calf Raises");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Triceps Extension");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Leg Extensions");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Leg Raises");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Overhead Press");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Rows");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Shoulder Press");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Hip Thrust");

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Name" },
                values: new object[] { 18, "Kettlebell Swing" });

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "Name", "Weight" },
                values: new object[,]
                {
                    { 1, "Hamstrings", 0 },
                    { 2, "Quadriceps", 0 },
                    { 3, "Glutes", 0 },
                    { 4, "Calves", 0 },
                    { 5, "Chest", 0 },
                    { 6, "Back", 0 },
                    { 7, "Shoulders", 0 },
                    { 8, "Biceps", 0 },
                    { 9, "Triceps", 0 },
                    { 10, "Forearms", 0 },
                    { 11, "Core", 0 },
                    { 12, "Trapezius", 0 },
                    { 13, "Adductors", 0 }
                });

            migrationBuilder.InsertData(
                table: "ExerciseMuscles",
                columns: new[] { "ExerciseId", "MuscleId", "Id" },
                values: new object[,]
                {
                    { 1, 5, 0 },
                    { 1, 7, 0 },
                    { 1, 9, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 0 },
                    { 2, 3, 0 },
                    { 2, 13, 0 },
                    { 3, 1, 0 },
                    { 3, 3, 0 },
                    { 3, 6, 0 },
                    { 3, 10, 0 },
                    { 3, 11, 0 },
                    { 3, 12, 0 },
                    { 4, 6, 0 },
                    { 4, 10, 0 },
                    { 5, 1, 0 },
                    { 5, 2, 0 },
                    { 5, 3, 0 },
                    { 5, 7, 0 },
                    { 5, 11, 0 },
                    { 5, 12, 0 },
                    { 6, 1, 0 },
                    { 6, 2, 0 },
                    { 6, 3, 0 },
                    { 7, 1, 0 },
                    { 7, 3, 0 },
                    { 7, 6, 0 },
                    { 8, 8, 0 },
                    { 8, 10, 0 },
                    { 9, 1, 0 },
                    { 9, 2, 0 },
                    { 9, 3, 0 },
                    { 10, 4, 0 },
                    { 11, 9, 0 },
                    { 12, 2, 0 },
                    { 13, 3, 0 },
                    { 13, 11, 0 },
                    { 14, 7, 0 },
                    { 14, 12, 0 },
                    { 15, 6, 0 },
                    { 15, 10, 0 },
                    { 15, 12, 0 },
                    { 16, 7, 0 },
                    { 16, 12, 0 },
                    { 17, 1, 0 },
                    { 17, 3, 0 },
                    { 17, 11, 0 },
                    { 18, 1, 0 },
                    { 18, 3, 0 },
                    { 18, 11, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseMuscles",
                table: "ExerciseMuscles");

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 3, 12 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 4, 10 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 5, 12 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 7, 6 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 8, 8 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 8, 10 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 11, 9 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 13, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 13, 11 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 14, 7 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 14, 12 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 6 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 10 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 15, 12 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 16, 7 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 16, 12 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 17, 11 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 1 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 3 });

            migrationBuilder.DeleteData(
                table: "ExerciseMuscles",
                keyColumns: new[] { "ExerciseId", "MuscleId" },
                keyValues: new object[] { 18, 11 });

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "ExerciseSessions",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AlterColumn<int>(
                name: "NofSets",
                table: "ExerciseSessions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "NofRepetitions",
                table: "ExerciseSessions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ExerciseMuscles",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseMuscles",
                table: "ExerciseMuscles",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Tlaky na lavičce");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Dřepy");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Mrtvý tah");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Shyby s váhou");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Přemístění");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Bulharský dřep");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7,
                column: "Name",
                value: "Rumunský mrtvý tah");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Biceps s jednoručkami");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 9,
                column: "Name",
                value: "Bench press s osou");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Výpady s váhou");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 11,
                column: "Name",
                value: "Calf Raises");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 12,
                column: "Name",
                value: "Triceps");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 13,
                column: "Name",
                value: "Leg Extensions");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 14,
                column: "Name",
                value: "Leg Raises");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 15,
                column: "Name",
                value: "Overhead Press");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 16,
                column: "Name",
                value: "Přítahy");

            migrationBuilder.UpdateData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 17,
                column: "Name",
                value: "Tlaky na ramena s jednoručkami");

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 19, "Hip Thrust" },
                    { 20, "Kettlebell Swing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscles_ExerciseId",
                table: "ExerciseMuscles",
                column: "ExerciseId");
        }
    }
}
