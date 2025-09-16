using GymLogger;
using System.Diagnostics.CodeAnalysis;

namespace GymLoggerTest
{
    public class CSVHandlerTest
    {
        [Fact]
        public void ReadLine()
        {
            //Arrange
            string input = """
                SessionName,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                Prsa,Bench Press,100,3,3,02.09.2025 11:30:26,note
                Prsa,Overhead Press,60,6,3,02.09.2025 11:30:26,
                """;
            CSVHandler handler = new CSVHandler();
            string expected = "Upload successful with success rate 2/0";
            //Act
            string actual = handler.PrepareForUpload(new CSVReader(new StringReader(input)), out int success, out int fail);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void MapColumns()
        {
            //Arrange
            string[] columnNamesActual = ["WEIGHT lifted", "time of session", "type of exercise", "number of repetitions", "number of SeTs", "name of session", "note for the future", "something something", "note231"];
            string[] columnNamesExpected = ["Weight", "DateTime", "ExerciseName", "Repetitions", "Sets", "SessionName", "Note", "invalidColumnName", "alreadyMappedColumn"];
            CSVHandler csvHandler = new CSVHandler();
            //Act
            string[] actual = new string[7];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = csvHandler.MapName(columnNamesActual[i]);
            }
            //Assert
            for (int i = 0; i < actual.Length; i++)
            {
                Assert.Equal(columnNamesExpected[i], actual[i]);
            }
        }
        [Fact]
        public void ColumnNames()
        {
            //Arrange
            CSVHandler csvHandler = new CSVHandler();
            string expected = "DateTime";
            //Act
            string actual = csvHandler.columnNames[7];
            //Assert
            Assert.Equal(expected, actual);
        }
    }
    public class CSVReaderTest
    {
        [Fact]
        public void ReadLine()
        {
            //Arrange
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                2,Prsa,1,Bench Press,100,3,3,,note
                2,Prsa,14,Overhead Press,60,6,3,02.09.2025 11:30:26,
                """;
            CSVReader reader = new CSVReader(new StringReader(input));
            //Act
            var result = reader.ReadLine();
            string actual1 = string.Join(",", result!);
            result = reader.ReadLine();
            string actual2 = string.Join(",", result!);
            result = reader.ReadLine();
            string actual3 = string.Join(",", result!);
            //Assert
            Assert.Equal("SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note", actual1);
            Assert.Equal("2,Prsa,1,Bench Press,100,3,3,,note", actual2);
            Assert.Equal("2,Prsa,14,Overhead Press,60,6,3,02.09.2025 11:30:26,", actual3);
        }
        [Fact]
        public void ReadLineEnd()
        {
            //Arrange
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                2,Prsa,1,Bench Press,100,3,3,,note
                """;
            CSVReader reader = new CSVReader(new StringReader(input));
            //Act
            var result = reader.ReadLine();
            string actual1 = string.Join(",", result!);
            result = reader.ReadLine();
            string actual2 = string.Join(",", result!);
            result = reader.ReadLine();
            //Assert
            Assert.Equal("SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note", actual1);
            Assert.Equal("2,Prsa,1,Bench Press,100,3,3,,note", actual2);
            Assert.Null(result);
        }
    }
}