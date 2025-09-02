using Microsoft.AspNetCore.Http;

namespace GymLogger
{
    public class CSVHandler
    {
        //Here I will prepare the string, File will be created in OnGetDownloadCsvAsync
        private string columnNames = "SessionId,SessionName,ExerciseId,ExerciseName,Weight,Repetitions,Sets,DateTime,Note";
        public string PrepareForDownload(List<Session> sessions)
        {
            string csvContent = "";
            var csvLines = new List<string>();
            csvLines.Add(columnNames);
            foreach (var session in sessions)
            {
                foreach (var es in session.ExerciseSessions!)
                {
                    csvLines.Add($"{es.SessionId},{es.Session!.Name},{es.ExerciseId},{es.Exercise?.Name},{es.Weight},{es.NofRepetitions},{es.NofSets},{es.DateTime},{es.Note}");
                }
            }
            csvContent += string.Join(Environment.NewLine, csvLines);
            return csvContent;
        }
    }
    public class CSVReader : TextReader
    {
        public CSVReader() { }
    }
}
