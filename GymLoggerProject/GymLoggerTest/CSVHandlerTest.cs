using GymLogger;

namespace GymLoggerTest
{
    public class CSVHandlerTest
    {
        [Fact]
        public void UploadTestSessionNames()
        {
            // Arrange
            var handler = new CSVHandler();
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                1,LegDay,2,Squat,105,10,3,02.09.2025 9:00:27,docela v pohode drepiky
                1,LegDay,13,Leg Raises,50,12,3,02.09.2025 9:00:50,najs nebo najs
                1,LegDay,6,Bulgarian Split Squat,125,10,3,02.09.2025 9:01:36,
                1,LegDay,10,Calf Raises,20,20,3,02.09.2025 9:07:17,Lejtka
                1,LegDay,13,Leg Raises,0,0,0,02.09.2025 9:26:59,
                1,LegDay,1,Bench Press,10,2,2,02.09.2025 9:38:22,
                3,Nohy,2,Squat,100,6,3,14.09.2025 13:27:07,
                3,Nohy,6,Bulgarian Split Squat,60,12,3,14.09.2025 13:27:26,
                3,Nohy,7,Romanian Deadlift,60,12,3,14.09.2025 13:27:45,
                3,Nohy,10,Calf Raises,20,12,6,14.09.2025 13:28:01,
                """;

            // Act
            var result = handler.PrepareForUpload(new CSVReader(new StringReader(input)), out int success, out int fail);

            // Assert
            Assert.Equal("LegDay", handler.sessions.Keys.ToList()[0]);
            Assert.Equal("Nohy", handler.sessions.Keys.ToList()[1]);
        }
        [Fact]
        public void UploadTestSessionVals()
        {
            // Arrange
            var handler = new CSVHandler();
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                1,LegDay,2,Squat,105,10,3,02.09.2025 9:00:27,docela v pohode drepiky
                1,LegDay,13,Leg Raises,50,12,3,02.09.2025 9:00:50,najs nebo najs
                1,LegDay,6,Bulgarian Split Squat,125,10,3,02.09.2025 9:01:36,
                1,LegDay,10,Calf Raises,20,20,3,02.09.2025 9:07:17,Lejtka
                1,LegDay,13,Leg Raises,0,0,0,02.09.2025 9:26:59,
                1,LegDay,1,Bench Press,10,2,2,02.09.2025 9:38:22,
                3,Nohy,2,Squat,100,6,3,14.09.2025 13:27:07,
                3,Nohy,6,Bulgarian Split Squat,60,12,3,14.09.2025 13:27:26,
                3,Nohy,7,Romanian Deadlift,60,12,3,14.09.2025 13:27:45,
                3,Nohy,10,Calf Raises,20,12,6,14.09.2025 13:28:01,
                """;

            // Act
            var _ = handler.PrepareForUpload(new CSVReader(new StringReader(input)), out int success, out int fail);

            // Assert
            Assert.Equal("najs nebo najs", handler.sessions["LegDay"][1].Note);
            Assert.Equal("Bulgarian Split Squat", handler.sessions["LegDay"][2].Exercise);
            Assert.Equal(125, handler.sessions["LegDay"][2].Weight);
            Assert.Equal(10, handler.sessions["LegDay"][2].Repetitions);
        }
        [Fact]
        public void UploadTestBase()
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
        public void UploadEmpty()
        {
            //Arrange
            string input = "";
            CSVHandler handler = new CSVHandler();
            string expected = "Upload failed: Empty File";
            //Act
            string actual = handler.PrepareForUpload(new CSVReader(new StringReader(input)), out int success, out int fail);
            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UploadTestMoreColumns()
        {
            //Arrange
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                1,LegDay,2,Squat,105,10,3,02.09.2025 9:00:27,docela v pohode drepiky
                1,LegDay,13,Leg Raises,50,12,3,02.09.2025 9:00:50,najs nebo najs
                1,LegDay,6,Bulgarian Split Squat,125,10,3,02.09.2025 9:01:36,
                1,LegDay,10,Calf Raises,20,20,3,02.09.2025 9:07:17,Lejtka
                1,LegDay,13,Leg Raises,0,0,0,02.09.2025 9:26:59,
                1,LegDay,1,Bench Press,10,2,2,02.09.2025 9:38:22,
                3,Nohy,2,Squat,100,6,3,14.09.2025 13:27:07,
                3,Nohy,6,Bulgarian Split Squat,60,12,3,14.09.2025 13:27:26,
                3,Nohy,7,Romanian Deadlift,60,12,3,14.09.2025 13:27:45,
                3,Nohy,10,Calf Raises,20,12,6,14.09.2025 13:28:01,
                """;
            CSVHandler handler = new CSVHandler();
            string expected = "Upload successful with success rate 10/0";
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
            string[] actual = new string[8];
            for (int i = 0; i < actual.Length; i++)
            {
                actual[i] = csvHandler.MapName(columnNamesActual[i], actual);
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
        [Fact]
        public void OrderTest()
        {
            //Arrange
            string[] inputKeys = ["WEIGHT lifted", "something something", "time of session",
                "type of exercise", "number of repetitions", "number of SeTs",
                "name of session", "note for the future", "note2231"];
            CSVHandler csvHandler = new CSVHandler();
            //Act
            Dictionary<string,int> actual = csvHandler.GetOrder(inputKeys);
            //Assert
            Assert.Equal(0, actual["Weight"]);
            Assert.Equal(2, actual["DateTime"]);
            Assert.Equal(3, actual["ExerciseName"]);
            Assert.Equal(4, actual["Repetitions"]);
            Assert.Equal(5, actual["Sets"]);
            Assert.Equal(6, actual["SessionName"]);
            Assert.Equal(7, actual["Note"]);
        }
        [Fact]
        public void ReadIncorrectInput()
        {
            //Arrange
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note
                1,LegDay,2,Squat,105,10,3,02.09.2025 9:00:27 docela v pohode drepiky
                1,LegDay,13,50,12,3,02.09.2025 9:00:50,najs nebo najs
                1,LegDay,6,Bulgarian Split Squat,125,10,3,02.09.2025 9:01:36,
                1,LegDay,13,Leg Raises,0,0,0,02.09.2025 9:26:59,
                1,LegDay,1,Bench Press,10,2,2,Tohle neni cas,
                3,Nohy,2,Squat,100,6,3,14.09.2025 13:27:07,
                3,Nohy,7,Romanian Deadlift,60,NAN,3,14.09.2025 13:27:45,
                3,Nohy,10,Calf Raises,thisisnotanumber,12,6,14.09.2025 13:28:01,
                """;
            CSVHandler handler = new CSVHandler();
            //Act
            string _ = handler.PrepareForUpload(new CSVReader(new StringReader(input)), out int success, out int fail);
            //Assert
            // Check if incorrect values got through the sieve
            // radky 1, 2, 5, 7, 8 by mely byt spatne z duvodu:
            // 1 : missing separator
            // 2 : missing ExerciseName, if there is correct columnCount,
            //      it will take the string on it's place,
            //      disregarding if it is a possible string,
            //      then it should stop being processed in Index.cshtml.cs
            // 5 : incorrect DateTime
            // 7 : string imparsable into integer on repetitions place
            // 8 : string imparsable into double on repetitions place
            // WARNING: NaN is parsable into double as double.NaN value
            Assert.Equal(5, fail);
            Assert.Equal(3, success);
            Assert.Equal(2,handler.sessions["LegDay"].Count);
            Assert.Single(handler.sessions["Nohy"]);
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
            var result = reader.GetKeys();
            string actual1 = string.Join(",", result!);
            result = reader.GetLine();
            string actual2 = string.Join(",", result!);
            result = reader.GetLine();
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
            var result = reader.GetKeys();
            string actual1 = string.Join(",", result!);
            result = reader.GetLine();
            string actual2 = string.Join(",", result!);
            result = reader.GetLine();
            //Assert
            Assert.Equal("SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note", actual1);
            Assert.Equal("2,Prsa,1,Bench Press,100,3,3,,note", actual2);
            Assert.Null(result);
        }
        [Fact]
        public void ReadEmpty()
        {
            //Arrange
            string input = "";
            CSVReader reader = new CSVReader(new StringReader(input));
            //Act
            var result = reader.GetKeys();
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void ReadEmptyRows()
        {
            //Arrange
            string input = """
                SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note

                2,Prsa,1,Bench Press,100,3,3,,note

                2,Prsa,14,Overhead Press,60,6,3,02.09.2025 11:30:26,
                """;
            CSVReader reader = new CSVReader(new StringReader(input));
            //Act
            var result = reader.GetKeys();
            string actual1 = string.Join(",", result!);
            result = reader.GetLine();
            var result1 = reader.GetLine();
            string actual2 = string.Join(",", result1!);
            result1 = reader.GetLine();
            var result2 = reader.GetLine();
            string actual3 = string.Join(",", result2!);
            result2 = reader.GetLine();
            var result3 = reader.GetLine();
            var result4 = reader.GetLine();
            //Assert actual values
            Assert.Equal("SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note", actual1);
            Assert.Equal("2,Prsa,1,Bench Press,100,3,3,,note", actual2);
            Assert.Equal("2,Prsa,14,Overhead Press,60,6,3,02.09.2025 11:30:26,", actual3);
            // Assert null values
            Assert.Null(result);
            Assert.Null(result1);
            Assert.Null(result2);
            Assert.Null(result3);
            Assert.Null(result4);
        }
    }
}