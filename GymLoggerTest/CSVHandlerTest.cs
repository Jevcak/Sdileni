using GymLogger;

namespace GymLoggerTest
{
    public class CSVHandlerTest
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
    }
}